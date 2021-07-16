package it.lume.mc.repository;

import java.util.List;

import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;

import it.lume.mc.model.Notifica;

public interface NotificaRepository extends CrudRepository<Notifica, Long>{
	
	@Query(nativeQuery = true, 
			value =   "SELECT notifica.* "
					+ "FROM notifica, utente "
					+ "WHERE notifica.utente_id = utente.id AND utente.id = ? ;")
	List<Notifica> findByUtenteId(Long utente_id);
	
	@Query(nativeQuery = true, 
			value =   "SELECT notifica.* "
					+ "FROM notifica, richiesta "
					+ "WHERE notifica.richiesta_id = richiesta.id AND richiesta.id = ? ;")
	List<Notifica> findByRichiestaId(Long richiesta_id);

	@Modifying
	@Query(nativeQuery = true, 
			value =   "DELETE FROM notifica "
					+ "WHERE notifica.soggetto_id = ?1 AND notifica.richiesta_id = ?2 ;")
	public void deleteAllByUtenteAndRichiesta(Long utente, Long richiesta);
}
