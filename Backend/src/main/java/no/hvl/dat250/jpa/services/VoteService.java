package no.hvl.dat250.jpa.services;

import no.hvl.dat250.jpa.DAO.VoteDAO;
import no.hvl.dat250.jpa.DAO.PollDAO;
import no.hvl.dat250.jpa.DAO.UserDAO;
import no.hvl.dat250.jpa.models.User;
import no.hvl.dat250.jpa.models.Poll;
import no.hvl.dat250.jpa.models.Vote;

import java.util.List;

    public class VoteService {

        PollDAO pollDAO = new PollDAO();
        UserDAO userDAO = new UserDAO();
        VoteDAO voteDAO = new VoteDAO();

        public List<Vote> readAllVotes() {
            return voteDAO.read();
        }

        public Vote readVote(Long id) {
            return voteDAO.read(id);
        }

        public boolean addVoteToPoll(Vote vote) {
            User user = userDAO.read(vote.getUser().getId());
            Poll poll = pollDAO.read(vote.getPoll().getId());
            if (user == null || poll == null) {
                return false;
            }
            Vote hasUserVoted = user.getVoted().stream().filter(p -> p.getId() == poll.getId()).findAny().orElse(null);
            if (hasUserVoted != null) {
                return false;
            }
            if (vote.getVote()) {
                poll.setPositiveVotes(poll.getPositiveVotes() + 1);
            } else {
                poll.setNegativeVotes(poll.getNegativeVotes() + 1);
            }
            pollDAO.update(poll);
            voteDAO.create(vote);
            return true;
        }

        public void delete(List<Vote> votes) {
            voteDAO.delete(votes);
        }
    }