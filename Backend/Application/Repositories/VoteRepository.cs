using Application.DTO.VoteDTOs;
using Application.Extensions;
using Application.Extentions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Repositories
{
    public interface IVoteRepository
    {
        Task<GetVoteDTO> GetVoteById(Guid Id);
        Task<List<GetVoteDTO>> GetVotesByPollId(Guid pollId);
        Task<List<GetVoteDTO>> GetYourVotes();
        Task<int> CreateVote(Guid pollId, CreateVoteDTO createVoteDTO);
        Task<int> ChangeVote(Guid VoteId, CreateVoteDTO updateVoteDTO);
        Task<int> DeleteVote(Guid VoteId);
    }
    public class VoteRepository : IVoteRepository
    {
        private readonly IGenericExtension _genericExtension;
        private readonly IFeedAppDbContext _context;
        private readonly IConfiguration _configuration;

        public VoteRepository(IGenericExtension genericExtension, IFeedAppDbContext context, IConfiguration configuration)
        {
            _genericExtension = genericExtension;
            _context = context;
            _configuration = configuration;
        }

        public async Task<GetVoteDTO> GetVoteById(Guid Id)
        {
            var response = await _context.Votes.Where(x => x.Id == Id).Select(p => new
            GetVoteDTO()
            {
                Id = p.Id,
                IsPositive = p.Positive,
                VotedPollId = p.Poll.Id,
                VotedPollQuestion = p.Poll.Question
            }).FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<GetVoteDTO>> GetVotesByPollId(Guid pollId)
        {
            var response = await _context.Votes.Where(x => x.Poll.Id == pollId).Select(p => new
            GetVoteDTO()
            {
                Id = p.Id,
                IsPositive = p.Positive,
                VotedPollId = p.Poll.Id,
                VotedPollQuestion = p.Poll.Question
            }).ToListAsync();
            return response;
        }

        public async Task<List<GetVoteDTO>> GetYourVotes()
        {
            var currentUser = await _genericExtension.GetCurrentUserAsync();
            var response = await _context.Votes.Where(x => x.User.Id == currentUser.Id).Select(p => new
            GetVoteDTO()
            {
                Id = p.Id,
                IsPositive = p.Positive,
                VotedPollId = p.Poll.Id,
                VotedPollQuestion = p.Poll.Question
            }).ToListAsync();
            return response;
        }

        public async Task<int> CreateVote(Guid pollId, CreateVoteDTO createVoteDTO)
        {
            Poll poll = await _context.Polls.Where(x => x.Id == pollId).FirstOrDefaultAsync();
            if (poll == null) { return -3; }
            Vote vote = createVoteDTO.ToVote();
            vote.Id = new Guid();
            if (poll.IsClosed) { return 0; }
            if (poll.IsPrivate)
            {
                User user = await _genericExtension.GetCurrentUserAsync();
                if (user == null) { return -1; }
                vote.User = user;
                var PollHasVoteByUser = await _context.Polls.Where(x => x.Id == pollId && x.Votes.Any(x => x.User == user)).FirstOrDefaultAsync();
                if (PollHasVoteByUser != null) { return -2; }
            }
            vote.Poll = poll;
            await _context.Votes.AddAsync(vote);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ChangeVote(Guid voteId, CreateVoteDTO updateVoteDTO)
        {
            if (updateVoteDTO.IsPositive == null) { return -1; }
            var currentUser = await _genericExtension.GetCurrentUserAsync();
            Vote dbVote = await _context.Votes.Where(x => x.Id == voteId).FirstOrDefaultAsync();
            if (dbVote == null) { return -2; }
            Poll dbPoll = await _context.Polls.Where(x => x.Votes.Any(y => y.Id == dbVote.Id)).FirstOrDefaultAsync();
            if (dbPoll.IsClosed) { return 0; }
            string dbVoterId = await _context.Votes.Where(x => x.Id == voteId).Select(x => x.User.Id).FirstOrDefaultAsync();
            if (dbVoterId != currentUser.Id) { return -1; }
            dbVote.Positive = updateVoteDTO.IsPositive;
            var update = _context.Votes.Update(dbVote);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteVote(Guid voteId)
        {
            var currentUser = await _genericExtension.GetCurrentUserAsync();
            Vote dbVote = await _context.Votes.Where(x => x.Id == voteId).FirstOrDefaultAsync();
            string dbVoterId = await _context.Votes.Where(x => x.Id == voteId).Select(x => x.User.Id).FirstOrDefaultAsync();
            if (dbVoterId != currentUser.Id) { return -1; }
            var delete = _context.Votes.Remove(dbVote);
            return await _context.SaveChangesAsync();
        }
    }
}
