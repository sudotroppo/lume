package it.lume.mc.repository;

import java.util.List;

import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import it.lume.mc.model.Richiesta;

@Repository
public interface RichiestaRepository extends CrudRepository<Richiesta, Long> {


	@Query(nativeQuery = true, 
			value =   "SELECT richiesta.* "
					+ "FROM richiesta, utente "
					+ "WHERE richiesta.creatore_id = utente.id AND utente.id = ? ;")
	public List<Richiesta> findByUtenteId(Long id);
	
	@Query(nativeQuery = true,
			value =   "SELECT * "
					+ "FROM richiesta "
					+ "ORDER BY data_creazione DESC "
					+ "LIMIT ?2 OFFSET ?1 ;")
	public List<Richiesta> findAllInRowRange(Long offset, Long row_count);
	
	

	@Query(nativeQuery = true,
			value =   "SELECT richiesta.* "
					+ "FROM richiesta LEFT JOIN utente_richieste_partecipate ON richiesta.id = utente_richieste_partecipate.richieste_partecipate_id "
					+ "GROUP BY utente_richieste_partecipate.richieste_partecipate_id, richiesta.id "
					+ "HAVING COUNT(utente_richieste_partecipate.candidati_id) < richiesta.numero_partecipanti ;")
	public List<Richiesta> findAll();


	@Query(nativeQuery = true,
			value =   "SELECT richiesta.* "
					+ "FROM richiesta, utente_richieste_partecipate "
					+ "WHERE richiesta.id = utente_richieste_partecipate.richieste_partecipate_id "
					+ "AND utente_richieste_partecipate.candidati_id = ? ;")
	public List<Richiesta> findPartecipazioniByUtenteId(Long id_utente);
	
	@Modifying
	@Query(nativeQuery = true,
			value =   "DELETE FROM notifica "
					+ "WHERE richiesta_id = ?1 ;"
					+ "DELETE FROM utente_richieste_partecipate "
					+ "WHERE richieste_partecipate_id = ?1 ;"
					+ "DELETE FROM richiesta "
					+ "WHERE richiesta.id = ?1 ;")
	public void deleteById(Long id);
}
