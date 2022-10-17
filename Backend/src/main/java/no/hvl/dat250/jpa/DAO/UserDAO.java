package no.hvl.dat250.jpa.DAO;

import no.hvl.dat250.jpa.models.User;

import javax.persistence.*;

import java.util.List;
public class UserDAO implements DAO<User> {

    private final EntityManager em;

    public UserDAO() {
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("default");
        em = factory.createEntityManager();
    }

    @Override
    public List<User> read() {
        TypedQuery<User> q = em.createQuery("Select u from User u", User.class);
        return q.getResultList();
    }

    @Override
    public User read(Long id) {
        return em.find(User.class, id);
    }

    @Override
    public void create(User user) {
        em.getTransaction().begin();
        em.persist(user);
        em.getTransaction().commit();
    }

    @Override
    public User update(User user) {
        //User u = em.find(User.class, id);

        em.getTransaction().begin();
        em.merge(user);
        em.getTransaction().commit();
        return user;
    }

    @Override
    public User delete(Long id) {
        User u = em.find(User.class, id);
        em.getTransaction().begin();
        em.remove(u);
        em.getTransaction().commit();
        return u;
    }

    @Override
    public void delete(List<User> users) {
        for (User user : users) {
            this.delete(user.getId());
        }
    }
}
