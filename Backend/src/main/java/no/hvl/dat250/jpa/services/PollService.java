package no.hvl.dat250.jpa.services;

import no.hvl.dat250.jpa.DAO.PollDAO;
import no.hvl.dat250.jpa.models.Poll;
import no.hvl.dat250.jpa.models.User;

import java.util.List;

public class PollService {

    PollDAO pollDAO = new PollDAO();

    public List<Poll> getAllPolls() {

        return pollDAO.read();
    }

    public Poll getPoll(Long id) {
        return pollDAO.read(id);
    }

    public boolean createPoll(Poll poll) {
        User user = poll.getCreator();
        if (user == null) {
            return false;
        }
        pollDAO.create(poll);
        return true;
    }

    public Poll deletePoll(Long id) {
        return pollDAO.delete(id);
    }

    public void delete(List<Poll> polls) {
        pollDAO.delete(polls);
    }
    public Poll updatePoll(Poll poll) {
        return pollDAO.update(poll);
    }
}
