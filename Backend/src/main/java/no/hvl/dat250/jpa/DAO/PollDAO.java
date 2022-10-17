package no.hvl.dat250.jpa.DAO;

import no.hvl.dat250.jpa.models.Poll;

import javax.persistence.*;

import java.util.List;

public class PollDAO implements DAO<Poll> {

    private final EntityManager em;

    public PollDAO() {
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("default");
        em = factory.createEntityManager();
    }

    @Override
    public List<Poll> read() {
        TypedQuery<Poll> q = em.createQuery("Select p from Poll p", Poll.class);
        return q.getResultList();
    }

    @Override
    public Poll read(Long id) {
        em.clear();
        return em.find(Poll.class, id);
    }

    @Override
    public void create(Poll poll) {
        em.getTransaction().begin();
        em.persist(poll);
        em.getTransaction().commit();
    }

    @Override
    public Poll update(Poll poll) {
        em.getTransaction().begin();
        em.merge(poll);
        em.getTransaction().commit();
        return poll;
    }

    @Override
    public Poll delete(Long id) {
        Poll p = em.find(Poll.class, id);
        em.getTransaction().begin();
        em.remove(p);
        em.getTransaction().commit();
        return p;
    }

    @Override
    public void delete(List<Poll> polls) {
        for (Poll poll : polls) {
            this.delete(poll.getId());
        }

    }
}