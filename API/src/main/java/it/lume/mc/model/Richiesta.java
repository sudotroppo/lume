package it.lume.mc.model;

import java.time.ZonedDateTime;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToMany;
import javax.persistence.ManyToOne;

import lombok.Data;

@Entity
public @Data class Richiesta {
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;
	
	private String titolo;
	
	private String descrizione;
	
	private Integer numeroPartecipanti;
	
	private ZonedDateTime dataCreazione;
	
	@ManyToOne
	private Utente creatore;
	
	@ManyToMany(mappedBy="richiestePartecipate", cascade = {CascadeType.REMOVE})
	private List<Utente> candidati;
	

	private String immagine;
	
	
	public Richiesta () {
		candidati = new ArrayList<>();
	}

	public boolean isDisponibile() {
		return candidati.size() < numeroPartecipanti;
	}
	
	public boolean allradyContains(Utente u) {
		return candidati.contains(u);
	}
	
	public void addPartecipante(Utente utente) {
		candidati.add(utente);
	}

	public void removePartecipante(Utente user) {
		candidati.remove(user);
	}
}
