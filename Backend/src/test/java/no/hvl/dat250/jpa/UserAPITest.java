package no.hvl.dat250.jpa;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
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


public class UserAPITest {
    private static final String PORT = "4687";
    private static final String baseURL = "http://localhost:" + PORT + "/";
    private static final MediaType JSON = MediaType.parse("application/json; charset=utf-8");
    private final OkHttpClient client = new OkHttpClient();
    private final Gson gson = new Gson();
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
    private User genericUser(){
        Random rd = new Random();
        User user = new User();
        user.setId(rd.nextLong());
        user.setFirstname("Test" + rd.nextInt(1000));
        user.setLastname("Testson"+ rd.nextInt(1000));
        user.setPassword("Testtest123"+ rd.nextInt(1000));
        user.setEmail("test@test.test"+ rd.nextInt(1000));
        return user;
    }

    @Test
    public void testCreateUser(){
        User user = genericUser();
        String postResult = postUser(user);
        User createdUser = gson.fromJson(postResult, User.class);

        // Parse returned user.
        final User returnedUser = gson.fromJson(getUser(createdUser.getId()), User.class);

        // The returned user must be the one we created earlier.
        assertNotNull(user.getId());
        assertThat(returnedUser, is(createdUser));
    }
    @Test
    public void testCreateMultipleUsers(){
        User user1 = genericUser();
        User user2 = genericUser();
        User user3 = genericUser();

        User createdUser1 = gson.fromJson(postUser(user1), User.class);
        User createdUser2 = gson.fromJson(postUser(user2), User.class);
        User createdUser3 = gson.fromJson(postUser(user3), User.class);

        ArrayList<User> allUsers = gson.fromJson(getAllUsers(), USER_LIST_TYPE);
        assertEquals(3, allUsers.size());
        assertTrue(allUsers.contains(createdUser1));
        assertTrue(allUsers.contains(createdUser2));
        assertTrue(allUsers.contains(createdUser3));
    }
    @Test
    public void testGetOneUser(){
        //Create a user
        User user = genericUser();
        User createdUser = gson.fromJson(postUser(user), User.class);
        //get request
        String getResult = getUser(createdUser.getId());
        User getUser = gson.fromJson(getResult, User.class);
        //Compare IDs
        assertThat(getUser, is(createdUser));
    }
    @Test
    public void testGetAllUsers(){
        User user1 = genericUser();
        User user2 = genericUser();
        User user3 = genericUser();
        postUser(user1);
        postUser(user2);
        postUser(user3);
        //Get all users:
        String getResult = getAllUsers();
        List<User> allUsers = gson.fromJson(getResult, USER_LIST_TYPE);
        assertEquals(3, allUsers.size());

    }
    private String getUser(Long id){
        Request rq = new Request.Builder()
                .url(baseURL + "users/" + id)
                .get()
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

    @Test
    public void testUpdateUser(){
        //Create a user
        User user = genericUser();
        User createdUser = gson.fromJson(postUser(user), User.class);
        //Update user with new data, same ID
        createdUser.setUsername("newUserName");
        createdUser.setFirstname("NewName");
        createdUser.setLastname("NewLastname");
        createdUser.setEmail("changed@email.com");
        User updatedUser = gson.fromJson(putUser(createdUser), User.class);
        //assert that the updated user shares ID with original user, but the rest of the info is updated:
        assertEquals(createdUser.getId(), updatedUser.getId());
        assertNotEquals(user.getUsername(), updatedUser.getUsername());
        assertNotEquals(user.getFirstname(), updatedUser.getFirstname());
        assertNotEquals(user.getLastname(), updatedUser.getLastname());
        assertNotEquals(user.getEmail(), updatedUser.getEmail());
        //Double check that we in fact updated the user and didn't accidentally create a new one
        String getUsers = getAllUsers();
        ArrayList<User> userList = gson.fromJson(getUsers, USER_LIST_TYPE);
        assertEquals(userList.size(), 1);
        assertTrue(userList.contains(updatedUser));
    }

    @Test
    public void testDeleteOneUser(){
        User user = genericUser();
        User createdUser = gson.fromJson(postUser(user), User.class);
        //Get size of all users in db
        ArrayList<User> userList = gson.fromJson(getAllUsers(), USER_LIST_TYPE);
        assertEquals(1, userList.size());
        //Delete above user:
        User deletedUser = gson.fromJson(deleteUser(createdUser.getId()), User.class);
        assertThat(deletedUser, is(createdUser));
        //Make sure it was deleted
        ArrayList<User> userListAfter = gson.fromJson(getAllUsers(), USER_LIST_TYPE);
        int afterUserCount = userListAfter.size();
        //assertThat(createdUser, is(deletedUser)); //Compare IDs
        assertEquals(0, afterUserCount);
    }
    private String postUser(User user){
        RequestBody body = RequestBody.create(gson.toJson(user), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "users")
                .post(body)
                .build();
        return doRequest(rq);
    }

    private String putUser(User user){
        RequestBody body = RequestBody.create(gson.toJson(user), JSON);
        Request rq = new Request.Builder()
                .url(baseURL + "users")
                .put(body)
                .build();
        return doRequest(rq);
    }

    private String deleteUser(Long id){
        Request rq = new Request.Builder()
                .url(baseURL + "users/" + id)
                .delete()
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
