package it.lume.mc.service;

import java.util.List;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import it.lume.mc.model.Notifica;
import it.lume.mc.repository.NotificaRepository;

@Service
public class NotificaService {
	
	@Autowired
	private NotificaRepository notificaRepository;
	
	@Transactional
	public Notifica getNotificaById(Long id) {
		return notificaRepository.findById(id).orElse(null);
	}
	
	@Transactional
	public List<Notifica> getNotificheByRichiesta(Long richiesta_id) {
		return notificaRepository.findByRichiestaId(richiesta_id);
	}
	
	@Transactional
	public List<Notifica> getNotificheByUtente(Long utente_id) {
		return notificaRepository.findByUtenteId(utente_id);
	}

	@Transactional
	public Boolean save(Notifica notifica) {
		return notificaRepository.save(notifica) != null;
	}

	@Transactional
	public void remove(Long id) {
		notificaRepository.deleteById(id);;
	}

	@Transactional
	public void removeByUtenteAndRichiesta(Long utente, Long richiesta) {
		notificaRepository.deleteAllByUtenteAndRichiesta(utente, richiesta);
	}
}
