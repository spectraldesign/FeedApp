package no.hvl.dat250.jpa;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import no.hvl.dat250.jpa.models.Poll;
import no.hvl.dat250.jpa.models.User;
import no.hvl.dat250.jpa.restapi.API;
import okhttp3.*;
import org.junit.After;
import org.junit.BeforeClass;
import org.junit.Test;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.junit.Assert.*;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.*;

public class PollAPITest {
    private static final String PORT = "4687";
    private static final String baseURL = "http://localhost:" + PORT + "/";
    private static final MediaType JSON = MediaType.parse("application/json; charset=utf-8");
    private final OkHttpClient client = new OkHttpClient();
    private final Gson gson = new Gson();

    private static final Type POLL_LIST_TYPE = new TypeToken<List<Poll>>() {}.getType();

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
    public void testCreatePoll(){
        User user = genericUser();
        String postUser = postUser(user);
        User creator = gson.fromJson(postUser, User.class);
        Poll poll = genericPoll();
        poll.setCreator(creator);
        String postResult2 = postRequest(poll);
        Poll createdPoll = gson.fromJson(postResult2, Poll.class);
        final Poll returnedPoll = gson.fromJson(getOnePoll(createdPoll.getId()), Poll.class);
        assertNotNull(createdPoll.getId());
        assertThat(createdPoll, is(returnedPoll));
    }

    @Test
    public void testCreateMultiplePolls(){
        User user = genericUser();
        String postUser = postUser(user);
        User creator = gson.fromJson(postUser, User.class);
        Poll poll1 = genericPoll();
        Poll poll2 = genericPoll();
        Poll poll3 = genericPoll();
        poll1.setCreator(creator);
        poll2.setCreator(creator);
        poll3.setCreator(creator);
        Poll createdPoll1 = gson.fromJson(postRequest(poll1), Poll.class);
        Poll createdPoll2 = gson.fromJson(postRequest(poll1), Poll.class);
        Poll createdPoll3 = gson.fromJson(postRequest(poll1), Poll.class);
        ArrayList<Poll> allPolls = gson.fromJson(getAllPolls(), POLL_LIST_TYPE);
        assertEquals(3,allPolls.size());
        assertTrue(allPolls.contains(createdPoll1));
        assertTrue(allPolls.contains(createdPoll2));
        assertTrue(allPolls.contains(createdPoll3));
    }

    @Test
    public void testGetOnePoll(){
        User user = genericUser();
        String postUser = postUser(user);
        User creator = gson.fromJson(postUser, User.class);
        //create poll:
        Poll poll = genericPoll();
        poll.setCreator(creator);
        Poll createdPoll = gson.fromJson(postRequest(poll), Poll.class);
        //get request:
        Poll resPoll = gson.fromJson(getOnePoll(createdPoll.getId()), Poll.class);
        assertNotNull(resPoll.getId());
        assertEquals(createdPoll, resPoll);
    }

    @Test
    public void testGetAllPolls(){
        User user = genericUser();
        User creator = gson.fromJson(postUser(user), User.class);
        Poll poll1 = genericPoll();
        Poll poll2 = genericPoll();
        Poll poll3 = genericPoll();
        poll1.setCreator(creator);
        poll2.setCreator(creator);
        poll3.setCreator(creator);
        Poll createdPoll1 = gson.fromJson(postRequest(poll1), Poll.class);
        Poll createdPoll2 = gson.fromJson(postRequest(poll2), Poll.class);
        Poll createdPoll3 = gson.fromJson(postRequest(poll3), Poll.class);

        ArrayList<Poll> allPolls = gson.fromJson(getAllPolls(), POLL_LIST_TYPE);
        assertEquals(3, allPolls.size());
        assertTrue(allPolls.contains(createdPoll1));
        assertTrue(allPolls.contains(createdPoll2));
        assertTrue(allPolls.contains(createdPoll3));
    }

    @Test
    public void testDeletePoll(){
        User user = genericUser();
        User creator = gson.fromJson(postUser(user), User.class);
        //Create poll
        Poll poll = genericPoll();
        poll.setCreator(creator);
        //Assert that poll exists
        Poll createdPoll = gson.fromJson(postRequest(poll), Poll.class);
        ArrayList<Poll> pollList = gson.fromJson(getAllPolls(), POLL_LIST_TYPE);
        assertEquals(1, pollList.size());
        assertNotNull(createdPoll.getId());
        //Delete poll
        gson.fromJson(deleteOnePoll(createdPoll.getId()), Poll.class);
        ArrayList<Poll> pollListAfter = gson.fromJson(getAllPolls(), POLL_LIST_TYPE);
        assertEquals(0, pollListAfter.size());
        //Make sure its deleted
    }

    @Test
    public void testUpdatePoll(){
        User user = genericUser();
        User creator = gson.fromJson(postUser(user), User.class);
        //create poll
        Poll poll = genericPoll();
        poll.setCreator(creator);
        //update poll
        Poll createdPoll = gson.fromJson(postRequest(poll), Poll.class);
        createdPoll.setQuestion("Did this update?");
        Poll updatedPoll = gson.fromJson(putRequest(createdPoll), Poll.class);
        //Get poll and make sure its updated
        //Poll updatedPoll = gson.fromJson(getOnePoll(createdPoll.getId()), Poll.class);
        assertNotNull(updatedPoll.getId());
        assertNotEquals(poll.getQuestion(), createdPoll.getQuestion());
        assertNotEquals(updatedPoll, poll);
        assertThat(createdPoll.getId(), is(updatedPoll.getId()));
        ArrayList<Poll> pollList = gson.fromJson(getAllPolls(), POLL_LIST_TYPE);
        assertEquals(1, pollList.size());
    }

    private String deleteOnePoll(Long id){
        Request rq = new Request.Builder()
                .url(baseURL + "polls/" + id)
                .delete()
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
    private String postRequest(Poll poll){
        RequestBody body = RequestBody.create(gson.toJson(poll), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "polls")
                .post(body)
                .build();
        return doRequest(rq);
    }

    private String putRequest(Poll poll){
        RequestBody body = RequestBody.create(gson.toJson(poll), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "polls")
                .put(body)
                .build();
        return doRequest(rq);
    }

    private String postUser(User user){
        RequestBody body = RequestBody.create(gson.toJson(user), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "users")
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
