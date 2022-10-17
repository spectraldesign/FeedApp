package no.hvl.dat250.jpa.DAO;

import no.hvl.dat250.jpa.models.Poll;

import java.util.List;

public interface DAO<T> {

    List<T> read();

    T read(Long id);

    void create(T t);

    T update(T t);

    T delete(Long id);

    void delete(List<T> t);
}
