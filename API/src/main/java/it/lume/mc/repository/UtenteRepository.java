package it.lume.mc.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import it.lume.mc.model.Utente;

@Repository
public interface UtenteRepository extends CrudRepository<Utente, Long>{

	public Optional<Utente> findByEmail(String email);
	
	public Boolean existsByEmail(String email);
	
	@Modifying
	@Query(nativeQuery = true,
	value =   "DELETE FROM notifica "
			+ "WHERE soggetto_id = ?1 ; "
			+ "DELETE FROM utente_richieste_partecipate "
			+ "WHERE candidati_id = ?1 ; "
			+ "DELETE FROM richiesta "
			+ "WHERE creatore_id = ?1 ; "
			+ "DELETE FROM utente "
			+ "WHERE id = ?1 ; ")
	public void deleteById(Long id);
}
