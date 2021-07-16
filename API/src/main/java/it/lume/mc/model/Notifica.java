package it.lume.mc.model;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.OneToOne;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import lombok.Data;

@Entity
public @Data class Notifica {
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;
	
	private String descrizione;
	
	@JsonIgnoreProperties(value = {"nome", "cognome", "email", "telefono", "notifiche", "richiestePartecipate", "richieste"})
	@ManyToOne
	private Utente utente;
	
	@JsonIgnoreProperties(value = {"nome", "cognome", "email", "telefono", "notifiche", "richiestePartecipate", "richieste"})
	@OneToOne
	private Utente soggetto;

	@JsonIgnoreProperties(value = {"titolo", "descrizione", "creatore", "candidati", "notifiche", "numeroPartecipanti", "dataCreazione"})
	@ManyToOne
	private Richiesta richiesta;

}
