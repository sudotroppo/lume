package it.lume.mc.service;

import javax.transaction.Transactional;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import it.lume.mc.model.Utente;
import it.lume.mc.repository.UtenteRepository;

@Service
public class UtenteService {

	@Autowired
	private UtenteRepository utenteRepository;

    @Autowired
    protected PasswordEncoder passwordEncoder;

    private final Logger logger = LoggerFactory.getLogger(this.getClass());
	
	@Transactional
	public Utente getByEmail(String email) {
		return utenteRepository.findByEmail(email).orElse(null);
	}
	
	@Transactional
	public Boolean save(Utente utente) {
		return utenteRepository.save(utente) != null;
	}
	
	@Transactional
	public void update(Utente nuovoUtente, Utente vecchioUtente) {
		
		vecchioUtente.setEmail(nuovoUtente.getEmail() == null || "".equals(nuovoUtente.getEmail()) ? vecchioUtente.getEmail() : nuovoUtente.getEmail());
		vecchioUtente.setNome(nuovoUtente.getNome() == null || "".equals(nuovoUtente.getNome()) ? vecchioUtente.getNome() : nuovoUtente.getNome());
		vecchioUtente.setCognome(nuovoUtente.getCognome() == null || "".equals(nuovoUtente.getCognome()) ? vecchioUtente.getCognome() : nuovoUtente.getCognome());
		vecchioUtente.setCitta(nuovoUtente.getCitta() == null || "".equals(nuovoUtente.getCitta()) ? vecchioUtente.getCitta() : nuovoUtente.getCitta());
		vecchioUtente.setTelefono(nuovoUtente.getTelefono() == null || "".equals(nuovoUtente.getTelefono()) ? vecchioUtente.getTelefono() : nuovoUtente.getTelefono());

		String pwd = nuovoUtente.getPassword();
		if(pwd != null) {
			vecchioUtente.setPassword(passwordEncoder.encode(pwd));
			
		}
	}

	@Transactional
	public Utente getById(Long utente_id) {
		return utenteRepository.findById(utente_id).orElse(null);
	}

	@Transactional
	public void remove(Long id) {
		utenteRepository.deleteById(id);
	}

	@Transactional
	public void registra(Utente utente) {
		utente.setPassword(passwordEncoder.encode(utente.getPassword()));
		utenteRepository.save(utente);
	}

	@Transactional
	public boolean existsById(Long id) {
		return utenteRepository.existsById(id);
	}

	@Transactional
	public boolean existsByEmail(String email) {
		return utenteRepository.existsByEmail(email);
	}

	public boolean checkPassword(String pwd, String password) {
		return passwordEncoder.matches(pwd, password);
	}

	@Transactional
	public void setPassword(String pwd, Utente user) {
		String newPwd = passwordEncoder.encode(pwd);
		user.setPassword(newPwd);
		
		utenteRepository.save(user);
	}
}
