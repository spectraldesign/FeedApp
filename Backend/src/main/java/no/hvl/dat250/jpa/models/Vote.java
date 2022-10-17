package no.hvl.dat250.jpa.models;

import com.google.gson.Gson;

import javax.persistence.*;

/**
 * Class that contains information about a vote.
 * Created to make handling voting through API easier.
 */
@Entity
@Table(name = "vote")
public class Vote {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    private boolean vote;

    @ManyToOne
    private User user;

    @ManyToOne
    private Poll poll;

    public Vote(){}

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public boolean getVote() {
        return vote;
    }

    public void setVote(boolean vote) {
        this.vote = vote;
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user){
        this.user = user;
    }

    public Poll getPoll() {
        return poll;
    }

    public void setPoll(Poll poll) {
        this.poll = poll;
    }

    public Object toJson() {
        Gson gson = new Gson();
        Object jsonObject = gson.toJson(this);
        return jsonObject;
    }
}
