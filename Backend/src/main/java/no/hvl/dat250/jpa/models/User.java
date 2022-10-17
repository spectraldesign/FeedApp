package no.hvl.dat250.jpa.models;

import com.google.gson.Gson;

import javax.persistence.*;
import java.util.*;

@Entity
@Table(name = "PollUser")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String firstname;
    private String lastname;
    private String email;
    private String username;
    private String password;

    @OneToMany(cascade= {CascadeType.ALL})
    @JoinColumn(name="votes")
    private Collection<Vote> votes = new ArrayList<Vote>();

    @OneToMany(mappedBy = "creator")
    private Set<Poll> polls = new HashSet<>();

    public Collection<Vote> getVoted() {
        return votes;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id){this.id = id;} //todo remove this when API uses JPA, only exists for testing purposes

    public String getFirstname() {
        return firstname;
    }

    public void setFirstname(String firstname) {
        this.firstname = firstname;
    }

    public String getLastname() {
        return lastname;
    }

    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public Collection<Vote> getVotedPolls() {
        return votes;
    }

    public void setVoted(Collection<Vote> voted) {
        this.votes = voted;
    }

    public void addVote(Vote vote){
        votes.add(vote);
    }

    public Set<Poll> getMyPolls() {
        return polls;
    }

    public void setMyPolls(Set<Poll> myPolls) {
        this.polls = myPolls;
    }

    public void addMyPoll(Poll poll){
        polls.add(poll);
    }

    @Override
    public boolean equals(Object o){
        if(this == o) return true;
        if(o == null || getClass() != o.getClass()) return false;
        User user = (User) o;
        return Objects.equals(id, user.id) && Objects.equals(username, user.username) && Objects.equals(email, user.email);
    }

    public Object toJson() {
        Gson gson = new Gson();
        Object jsonObject = gson.toJson(this);
        return jsonObject;
    }
}
