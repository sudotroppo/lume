package it.lume.mc.service;

import java.util.List;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import it.lume.mc.model.Richiesta;
import it.lume.mc.repository.RichiestaRepository;

@Service
public class RichiestaService {
	
	@Autowired
	private RichiestaRepository richiestaRepository; 
	
	@Transactional
	public List<Richiesta> getAllRichieste() {
		
		return (List<Richiesta>)richiestaRepository.findAll();
	}

	//richieste in ordine di data decrescente con numero di riga che va da offset (escluso) a row_count + offset
	@Transactional
	public List<Richiesta> getRichiesteInRowRange(Long offset, Long row_count) {
		return richiestaRepository.findAllInRowRange(offset, row_count);
	}

	@Transactional
	public List<Richiesta> getPartecipazioni(Long id_utente){
		return richiestaRepository.findPartecipazioniByUtenteId(id_utente);
	}
	
	@Transactional
	public Richiesta getRichiesta(Long id) {
		return richiestaRepository.findById(id).orElse(null);
	}
	
	@Transactional
	public Richiesta save(Richiesta richiesta) {
		return richiestaRepository.save(richiesta);
	}

	@Transactional
	public List<Richiesta> getRichiesteByUtenteId(Long id) {
		return richiestaRepository.findByUtenteId(id);
	}

	@Transactional
	public void remove(Long id) {
		richiestaRepository.deleteById(id);
	}
	@Transactional
	public boolean exists(Long id) {
		return richiestaRepository.existsById(id);
	}
}
