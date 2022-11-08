using Application.DTO.PollDTOs;
using Application.Extentions;
using Domain.Entities;
using Domain.Interfaces;
using IdGen;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Messaging;

namespace Application.Repositories
{
    public interface IPollRepository
    {
        Task<GetPollDTO> GetPollById(string id);
        Task<List<GetPollDTO>> GetAllPolls();
        Task<int> CreatePoll(CreatePollDTO createPollDTO);
        Task<int> UpdatePollAsync(string Id, UpdatePollDTO updatePollDTO);
        Task<int> DeletePollAsync(string pollId);
        Task<int> ClosePollAsync(string pollId);

    }
    public class PollRepository : IPollRepository
    {
        private readonly IGenericExtension _genericExtension;
        private readonly IFeedAppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IdGenerator _idGenerator;

        protected RabbitMQClient _rabbitMQClient = new RabbitMQClient();
        
        public PollRepository(IGenericExtension genericExtension, IFeedAppDbContext context, IConfiguration configuration, IdGenerator idGenerator)

        {
            _genericExtension = genericExtension;
            _context = context;
            _configuration = configuration;
            _idGenerator = idGenerator;
        }

        public async Task<GetPollDTO> GetPollById(string id)
        {
            var response = await _context.Polls.Where(x => x.Id == id).Select(p => new
            GetPollDTO()
            {
                Id = p.Id,
                Question = p.Question,
                IsPrivate = p.IsPrivate,
                IsClosed = p.IsClosed,
                EndTime = p.EndTime,
                CreatorId = p.Creator.Id,
                CreatorName = p.Creator.UserName,
                CountVotes = p.Votes.Count,
                PositiveVotes = p.Votes.Where(v => v.Positive == true).Count(),
                NegativeVotes = p.Votes.Where(v => v.Positive == false).Count()
            }).FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<GetPollDTO>> GetAllPolls()
        {
            var response = await _context.Polls.Select(p => new
            GetPollDTO()
            {
                Id = p.Id,
                Question = p.Question,
                IsPrivate = p.IsPrivate,
                IsClosed = p.IsClosed,
                EndTime = p.EndTime,
                CreatorId = p.Creator.Id,
                CreatorName = p.Creator.UserName,
                CountVotes = p.Votes.Count,
                PositiveVotes = p.Votes.Where(v => v.Positive == true).Count(),
                NegativeVotes = p.Votes.Where(v => v.Positive == false).Count()
            }).ToListAsync();
            return response;
        }

        public async Task<int> CreatePoll(CreatePollDTO createPollDTO)
        {
            User user = await _genericExtension.GetCurrentUserAsync();
            var poll = createPollDTO.ToPoll();
            poll.Id = _idGenerator.CreateId().ToString();
            poll.Creator = user;
            poll.Votes = new List<Vote>();
            await _context.Polls.AddAsync(poll);
            var saved = await _context.SaveChangesAsync();
            Console.WriteLine("saved" + saved);
            Console.WriteLine("poll" + poll.Question);
            _rabbitMQClient.PublishNewPoll(poll);
            return saved;
        }

        public async Task<int> UpdatePollAsync(string pollId, UpdatePollDTO updatePoll)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            if (dbPoll == null) { return -2; }
            var pollCreatorId = await _context.Polls.Where(x => x.Id == pollId).Select(x => x.Creator.Id).FirstOrDefaultAsync();
            if (dbPoll.IsClosed) { return 0; } //If the poll is closed it should not be editable
            if ((pollCreatorId != currentUser.Id) && !currentUser.IsAdmin) //You should only be allowed to edit your own polls, unless you are admin.
            {
                return -1;
            }
            if (updatePoll.Question != null) { dbPoll.Question = updatePoll.Question; }
            if (updatePoll.IsPrivate != null) { dbPoll.IsPrivate = updatePoll.IsPrivate; }
            if (updatePoll.EndTime != null) { dbPoll.EndTime = updatePoll.EndTime; }
            var update = _context.Polls.Update(dbPoll);
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeletePollAsync(string pollId)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            if (dbPoll == null) { return -1; }
            var pollCreatorId = await _context.Polls.Where(x => x.Id == pollId).Select(x => x.Creator.Id).FirstOrDefaultAsync();
            if ((pollCreatorId != currentUser.Id) && !currentUser.IsAdmin) { return -2; }
            var delete = _context.Polls.Remove(dbPoll);
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async Task<int> ClosePollAsync(string pollId)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            dbPoll.Votes = await _context.Votes.Where(x => x.Poll.Id == pollId).ToListAsync();
            if (dbPoll == null) { return -1; }
            var pollCreatorId = await _context.Polls.Where(x => x.Id == pollId).Select(x => x.Creator.Id).FirstOrDefaultAsync();
            if ((pollCreatorId != currentUser.Id) && !currentUser.IsAdmin) { return -2; }
            dbPoll.IsClosed = true;
            var update = _context.Polls.Update(dbPoll);
            var res = await _context.SaveChangesAsync();

            PollResult pollResult = new PollResult(
            );
            pollResult.Id = dbPoll.Id;
            pollResult.Question = dbPoll.Question;
            pollResult.PositiveVotes = dbPoll.Votes.Where(x => x.Positive == true).Count();
            pollResult.NegativeVotes = dbPoll.Votes.Where(x => x.Positive == false).Count();
            pollResult.TotalVotes = dbPoll.Votes.Count;

            _rabbitMQClient.PublishClosedPoll(pollResult);
            return res;
        }
    }
}
