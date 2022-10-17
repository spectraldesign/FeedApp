package no.hvl.dat250.jpa.DAO;

import no.hvl.dat250.jpa.models.Vote;

import javax.persistence.*;

import java.util.List;

public class VoteDAO implements DAO<Vote> {

    private final EntityManager em;

    public VoteDAO() {
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("default");
        em = factory.createEntityManager();
    }

    @Override
    public List<Vote> read() {
        TypedQuery<Vote> q = em.createQuery("SELECT v from Vote v", Vote.class);
        return q.getResultList();
    }

    @Override
    public Vote read(Long id) {
        return em.find(Vote.class, id);
    }

    @Override
    public void create(Vote vote) {
        em.getTransaction().begin();
        em.persist(vote);
        em.getTransaction().commit();
    }

    @Override
    public Vote update(Vote vote) {
        //Vote v = em.find(Vote.class, id);
        em.getTransaction().begin();
        em.merge(vote);
        em.getTransaction().commit();
        return vote;
    }

    @Override
    public Vote delete(Long id) {
        Vote v = em.find(Vote.class, id);
        em.getTransaction().begin();
        em.remove(v);
        em.getTransaction().commit();
        return v;
    }

    @Override
    public void delete(List<Vote> votes) {
        for (Vote vote : votes) {
            this.delete(vote.getId());
        }

    }
}