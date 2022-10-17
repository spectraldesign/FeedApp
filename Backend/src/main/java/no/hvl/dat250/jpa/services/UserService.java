package no.hvl.dat250.jpa.services;

import java.util.List;
import no.hvl.dat250.jpa.DAO.UserDAO;
import no.hvl.dat250.jpa.models.User;

public class UserService {

    UserDAO userDAO = new UserDAO();

    public List<User> getAllUsers() {

        return userDAO.read();
    }

    public User getUserById(Long id) {
        return userDAO.read(id);
    }

    public void createUser(User user) {
        userDAO.create(user);
    }

    public User deleteUser(Long id) {
        return userDAO.delete(id);
    }

    public User updateUser(User user) {
        return userDAO.update(user);
    }

    public void delete(List<User> users) {
        userDAO.delete(users);
    }
}