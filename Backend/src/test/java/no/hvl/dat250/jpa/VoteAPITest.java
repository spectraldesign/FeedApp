package no.hvl.dat250.jpa;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import no.hvl.dat250.jpa.models.Poll;
import no.hvl.dat250.jpa.models.User;
import no.hvl.dat250.jpa.models.Vote;
import no.hvl.dat250.jpa.restapi.API;
import okhttp3.*;
import org.junit.After;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.*;

public class VoteAPITest {
    private static final String PORT = "4687";
    private static final String baseURL = "http://localhost:" + PORT + "/";
    private static final MediaType JSON = MediaType.parse("application/json; charset=utf-8");
    private final OkHttpClient client = new OkHttpClient();
    private final Gson gson = new Gson();
    private static final Type POLL_LIST_TYPE = new TypeToken<List<Poll>>() {}.getType();
    private static final Type USER_LIST_TYPE = new TypeToken<List<User>>() {}.getType();
    @BeforeClass
    public static void startRestServer() {
        API.main(new String[]{PORT});
    }

    /**
     * Reset database between each test to make testing easier, as we start with a fresh slate each test.
     */
    @After
    public void resetDB(){
        Request rq = new Request.Builder()
                .url(baseURL + "deleteAll")
                .delete()
                .build();
        doRequest(rq);
    }

    private Poll genericPoll(){
        Random random = new Random();
        Poll poll = new Poll();
        poll.setQuestion("Is " + random.nextInt(100000) + " a nice number?");
        poll.setEndTime(random.nextLong());
        poll.setPositiveVotes(random.nextInt(100));
        poll.setNegativeVotes(random.nextInt(80));
        poll.setPrivate(false);
        return poll;
    }
    private User genericUser(){
        Random rd = new Random();
        User user = new User();
        user.setFirstname("Test" + rd.nextInt(1000));
        user.setLastname("Testson"+ rd.nextInt(1000));
        user.setPassword("Testtest123"+ rd.nextInt(1000));
        user.setEmail("test@test.test"+ rd.nextInt(1000));
        return user;
    }

    @Test
    public void testVotesOnPolls() {
        //Initialize
        User user1 = genericUser();
        User user2 = genericUser();
        Poll poll1 = genericPoll();
        Poll poll2 = genericPoll();

        User creator1 = gson.fromJson(postUser(user1), User.class);
        User creator2 = gson.fromJson(postUser(user2), User.class);
        poll1.setCreator(creator1);
        poll2.setCreator(creator2);

        //Get poll info
        Poll createdPoll1 = gson.fromJson(postPoll(poll1), Poll.class);
        Poll createdPoll2 = gson.fromJson(postPoll(poll2), Poll.class);

        //Vote on polls
        Vote vote1 = new Vote();
        vote1.setPoll(createdPoll1);
        vote1.setUser(creator1);
        vote1.setVote(false);

        Vote vote2 = new Vote();
        vote2.setPoll(createdPoll2);
        vote2.setUser(creator2);
        vote2.setVote(true);

        postVote(vote1);
        postVote(vote2);
        Long id = createdPoll1.getId();
        //Get updated polls
        Poll votedPoll1 = gson.fromJson(getOnePoll(id), Poll.class);
        Poll votedPoll2 = gson.fromJson(getOnePoll(createdPoll2.getId()), Poll.class);

        //Assert that everything updated
        assertEquals(createdPoll1.getId(), votedPoll1.getId());
        assertEquals(createdPoll1.getPositiveVotes(), votedPoll1.getPositiveVotes());
        assertEquals(createdPoll1.getNegativeVotes()+1, votedPoll1.getNegativeVotes());
        assertEquals(createdPoll2.getId(), votedPoll2.getId());
        assertEquals(createdPoll2.getPositiveVotes()+1, votedPoll2.getPositiveVotes());
        assertEquals(createdPoll2.getNegativeVotes(), votedPoll2.getNegativeVotes());
        //Assert that amount is correct for everything (that we didn't create duplicates)
        ArrayList<Poll> allPolls = gson.fromJson(getAllPolls(), POLL_LIST_TYPE);
        ArrayList<User> allUsers = gson.fromJson(getAllUsers(), USER_LIST_TYPE);
        assertTrue(allUsers.size() == allPolls.size() && allPolls.size() == 2);
    }

    private String postUser(User user){
        RequestBody body = RequestBody.create(gson.toJson(user), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "users")
                .post(body)
                .build();
        return doRequest(rq);
    }

    private String getAllUsers(){
        Request rq = new Request.Builder()
                .url(baseURL + "users")
                .get()
                .build();
        return doRequest(rq);
    }
    private String postPoll(Poll poll){
        RequestBody body = RequestBody.create(gson.toJson(poll), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "polls")
                .post(body)
                .build();
        return doRequest(rq);
    }

    private String getOnePoll(Long id){
        Request rq = new Request.Builder()
                .url(baseURL + "polls/" + id)
                .get()
                .build();
        return doRequest(rq);
    }
    private String getAllPolls(){
        Request rq = new Request.Builder()
                .url(baseURL + "polls")
                .get()
                .build();
        return doRequest(rq);
    }

    private String postVote(Vote vote){
        RequestBody body = RequestBody.create(gson.toJson(vote), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "vote")
                .post(body)
                .build();
        return doRequest(rq);
    }

    private String doRequest(Request request){
        try (Response response = client.newCall(request).execute()) {
            return Objects.requireNonNull(response.body()).string();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
