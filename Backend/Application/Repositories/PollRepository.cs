using Application.DTO;
using Application.Extentions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Repositories
{
    public interface IPollRepository
    {
        Task<GetPollDTO> GetPollById(Guid id);
        Task<List<GetPollDTO>> GetAllPolls();
        Task<int> CreatePoll(CreatePollDTO createPollDTO);
        Task<int> UpdatePollAsync(Guid Id, CreatePollDTO updatePollDTO);
        Task<int> DeletePollAsync(Guid pollId);
        Task<int> ClosePollAsync(Guid pollId);

    }
    public class PollRepository : IPollRepository
    {
        private readonly IGenericExtension _genericExtension;
        private readonly IFeedAppDbContext _context;
        private readonly IConfiguration _configuration;

        public PollRepository(IGenericExtension genericExtension, IFeedAppDbContext context, IConfiguration configuration)
        {
            _genericExtension = genericExtension;
            _context = context;
            _configuration = configuration;
        }

        public async Task<GetPollDTO> GetPollById(Guid id)
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
                CountVotes = p.Votes.Count
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
                CountVotes = p.Votes.Count
            }).ToListAsync();
            return response;
        }

        public async Task<int> CreatePoll(CreatePollDTO createPollDTO)
        {
            User user = await _genericExtension.GetCurrentUserAsync();
            var poll = createPollDTO.ToPoll();
            poll.Id = new Guid();
            poll.Creator = user;
            poll.Votes = new List<Vote>();
            await _context.Polls.AddAsync(poll);
            var saved = await _context.SaveChangesAsync();
            return saved;
        }

        public async Task<int> UpdatePollAsync(Guid pollId, CreatePollDTO updatePoll)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
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

        public async Task<int> DeletePollAsync(Guid pollId)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            var pollCreatorId = await _context.Polls.Where(x => x.Id == pollId).Select(x => x.Creator.Id).FirstOrDefaultAsync();
            if ((pollCreatorId != currentUser.Id) && !currentUser.IsAdmin) { return -1; }
            var delete = _context.Polls.Remove(dbPoll);
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async Task<int> ClosePollAsync(Guid pollId)
        {
            User currentUser = await _genericExtension.GetCurrentUserAsync();
            var dbPoll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            var pollCreatorId = await _context.Polls.Where(x => x.Id == pollId).Select(x => x.Creator.Id).FirstOrDefaultAsync();
            if ((pollCreatorId != currentUser.Id) && !currentUser.IsAdmin) { return -1; }
            dbPoll.IsClosed = true;
            var update = _context.Polls.Update(dbPoll);
            var res = await _context.SaveChangesAsync();
            return res;
        }
    }
}
