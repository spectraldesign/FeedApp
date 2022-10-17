package no.hvl.dat250.jpa.models;

import com.google.gson.Gson;

import javax.persistence.*;
import java.util.*;

@Entity
@Table(name = "Poll")
public class Poll {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "creator_id")
    private User creator;

    @OneToMany(mappedBy = "poll")
    private Set<Vote> votes = new HashSet<Vote>();

    public void addVote(Vote vote){
        votes.add(vote);
    }

    private String question;
    private boolean isPrivate;
    private boolean isClosed;
    private long endTime; //Time in number of milliseconds since January 1, 1970, 00:00:00 GMT (the default for Java Date object)
    private int positiveVotes;
    private int negativeVotes;

    public Long getId() {
        return id;
    }
    public void setId(Long id) {
        this.id = id;
    }

    public User getCreator() {
        return creator;
    }

    public void setCreator(User creator) {
        this.creator = creator;
    }

    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }

    public boolean isPrivate() {
        return isPrivate;
    }

    public void setPrivate(boolean aPrivate) {
        isPrivate = aPrivate;
    }

    public boolean isClosed() {
        return isClosed;
    }

    public void setClosed(boolean closed) {
        isClosed = closed;
    }

    public long getEndTime() {
        return endTime;
    }

    public void setEndTime(long endTime) {
        this.endTime = endTime;
    }


    public int getPositiveVotes() {
        return this.positiveVotes;
    }

    public void setPositiveVotes(int positiveVotes) {
        this.positiveVotes = positiveVotes;
    }

    public int getNegativeVotes() {
        return this.negativeVotes;
    }

    public void setNegativeVotes(int negativeVotes) {
        this.negativeVotes = negativeVotes;
    }

    @Override
    public boolean equals(Object o){
        if(this == o) return true;
        if(o == null || getClass() != o.getClass()) return false;
        Poll poll = (Poll) o;
        return Objects.equals(id, poll.id) && Objects.equals(question, poll.question) && Objects.equals(creator, poll.creator);
    }

    public Object toJson() {
        Gson gson = new Gson();
        Object jsonObject = gson.toJson(this);
        return jsonObject;
    }
}
