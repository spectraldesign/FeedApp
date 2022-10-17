package no.hvl.dat250.jpa.restapi;
import no.hvl.dat250.jpa.models.Poll;
import no.hvl.dat250.jpa.models.User;
import com.google.gson.Gson;
import no.hvl.dat250.jpa.models.Vote;
import no.hvl.dat250.jpa.services.PollService;
import no.hvl.dat250.jpa.services.UserService;
import no.hvl.dat250.jpa.services.VoteService;

import static spark.Spark.*;

public class API {
    static PollService pollService = new PollService();
    static UserService userService = new UserService();
    static VoteService voteService = new VoteService();
    static Gson gson = new Gson();
    public static void main(String[] args) {
        if (args.length > 0) {
            port(Integer.parseInt(args[0]));
        } else {
            port(8080);
            System.out.println("Deployed to http://localhost:8080");
        }
        get("/", (req,res) -> "This is the home page :)");

        //User listeners:
        //Create user
        post("/users", (req, res) -> {
            User user = gson.fromJson(req.body(), User.class);
            userService.createUser(user);
            return gson.toJson(user);
        });
        //Update user
        put("/users", (req, res) -> {
            User user = gson.fromJson(req.body(), User.class);
            try{
                if(user == null){
                    return gson.toJson("User does not exist.");
                }
                return gson.toJson(userService.updateUser(user));
            }
            catch (Exception e){
                return gson.toJson("Not a valid user ID!");
            }
        });

        //Get all users
        get("/users", (req,res) ->
                gson.toJsonTree(userService.getAllUsers())
        );

        //Get a user by id
        get("/users/:id", (req,res) -> {
            String idString = req.params(":id");
            try{
                Long id = Long.parseLong(idString);
                if(userService.getUserById(id) == null){
                    return gson.toJson("User with id "+id+" does not exist.");
                }
                return gson.toJson(userService.getUserById(id));
            }
            catch (Exception e){
                return gson.toJson("Not a valid user ID!");
            }
        });

        //Delete a user by id
        delete("/users/:id", (req, res) -> {
            String idString = req.params(":id");
            try{
                Long id = Long.parseLong(idString);
                if(userService.getUserById(id) == null){
                    return gson.toJson("User with id "+id+" does not exist.");
                }
                return gson.toJson(userService.deleteUser(id));
            }
            catch (Exception e){
                return gson.toJson("Not a valid user ID!");
            }
        });

        get("/register", (req,res) -> "Register user here");

        get("/login", (req,res) -> "Login here");       

        //Poll listeners:
        //Create and update poll
        post("/polls", (req,res) -> {
            Poll poll = gson.fromJson(req.body(), Poll.class);
            if (pollService.createPoll(poll)) {
                return gson.toJson(poll);
            }
            else
                return "No User found with this id";
        });

        //Poll listeners:
        //Create and update poll
        put("/polls", (req,res) -> {
            Poll poll = gson.fromJson(req.body(), Poll.class);
            try{
                if(poll == null){
                    return gson.toJson("Poll with does not exist.");
                }
                return gson.toJson(pollService.updatePoll(poll));
            }
            catch (Exception e){
                return gson.toJson("Not a valid poll ID!");
            }
        });

        //Get all polls
        get("/polls", (req,res) -> gson.toJsonTree(pollService.getAllPolls()));

        //Get a poll by id
        get("/polls/:id", (req, res) -> {
            String idString = req.params(":id");
            try{
                Long id = Long.parseLong(idString);
                if(pollService.getPoll(id) == null){
                    return gson.toJson("Poll with id "+id+" does not exist.");
                }
                return gson.toJson(pollService.getPoll(id));
            }
            catch (Exception e){
                return gson.toJson("Not a valid poll ID!");
            }
        });
        //Delete a poll by id
        delete("/polls/:id", (req, res) -> {
            String idString = req.params(":id");
            try{
                Long id = Long.parseLong(idString);
                if(pollService.getPoll(id) == null){
                    return gson.toJson("Poll with id "+id+" does not exist.");
                }

                return gson.toJson(pollService.deletePoll(id));
            }
            catch (Exception e){
                return gson.toJson("Not a valid poll ID!");
            }
        });

        //Get all votes
        get("/vote", (req, res) ->
            gson.toJsonTree(voteService.readAllVotes()));

        //Get one vote
        get("/vote/:id", (req, res) -> {
            String idString = req.params(":id");
            try{
                Long id = Long.parseLong(idString);
                if(voteService.readVote(id) == null){
                    return gson.toJson("Vote with id "+id+" does not exist.");
                }
                return gson.toJson(voteService.readVote(id));
            }
            catch (Exception e){
                return gson.toJson("Not a valid poll ID!");
            }
        });


        //Vote on a poll
        post("/vote", (req, res) -> {
            Vote vote = gson.fromJson(req.body(), Vote.class);
            if(!voteService.addVoteToPoll(vote)){
                return gson.toJson("User already voted or user/poll not found");
            }
            return gson.toJson("Vote registered successfully");
        });


        //Delete all entries (Only for testing purposes!!)
        delete("/deleteAll", (req, res) -> {
            voteService.delete(voteService.readAllVotes());
            pollService.delete(pollService.getAllPolls());
            userService.delete(userService.getAllUsers());

            return gson.toJson("Deleted all data");
        });
    }
}
