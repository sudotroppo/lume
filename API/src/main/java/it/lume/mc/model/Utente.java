package it.lume.mc.model;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToMany;
import javax.persistence.OneToMany;

import com.fasterxml.jackson.annotation.JsonIgnore;

import lombok.Data;

@Entity
public @Data class Utente {
	
	public static final String USER_ROLE = "UTENTE";
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;
	
	@Column(unique = true)
	private String email;
	
	private String nome;
	
	private String cognome;
	
	private String telefono;
	
	private String citta;
	
	private String immagine;

	@JsonIgnore
	private String password;

	@JsonIgnore
	private String ruolo;

	@JsonIgnore
	private Boolean enabled;
	
	@JsonIgnore
	@OneToMany(mappedBy = "utente", fetch = FetchType.LAZY, cascade = {CascadeType.REMOVE})
	private List<Notifica> notifiche;

	@JsonIgnore
	@ManyToMany
	private List<Richiesta> richiestePartecipate;

	@JsonIgnore
	@OneToMany(mappedBy = "creatore", fetch = FetchType.LAZY, cascade = {CascadeType.REMOVE})
	private List<Richiesta> richieste;
	
	public Utente() {
		notifiche = new ArrayList<>();
		richiestePartecipate = new ArrayList<>();
		richieste = new ArrayList<>();
		
	}
	
	public void addPartecipazioneAProposta(Richiesta richiesta) {
		richiestePartecipate.add(richiesta);
	}

	public void rimuoviPartecipazione(Richiesta r) {
		richiestePartecipate.remove(r);
	}
	
	

}
