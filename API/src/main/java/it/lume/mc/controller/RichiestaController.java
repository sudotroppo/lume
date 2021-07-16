package it.lume.mc.controller;

import java.time.ZonedDateTime;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import it.lume.mc.controller.validator.RichiestaValidator;
import it.lume.mc.model.Notifica;
import it.lume.mc.model.Richiesta;
import it.lume.mc.model.Utente;
import it.lume.mc.service.NotificaService;
import it.lume.mc.service.RichiestaService;
import it.lume.mc.service.UtenteService;

@RestController
public class RichiestaController {

	@Autowired
	private RichiestaService richiestaService;
	
	@Autowired
	private RichiestaValidator richiestaValidator;
	
	@Autowired
	private NotificaService notificaService;

	@Autowired
	private UtenteService utenteService;

	@RequestMapping(value="/public/richiesta", method = RequestMethod.GET)
	public List<Richiesta> getAllRichieste() {
		return richiestaService.getAllRichieste();
	}

	@RequestMapping(value="/public/richiesta/{id}", method = RequestMethod.GET)
	public Richiesta getRichiesta(@PathVariable("id") Long id) {
		return richiestaService.getRichiesta(id);
	}
	
	@RequestMapping(value="/protected/richiesta/utente", method = RequestMethod.GET)
	public ResponseEntity<?> getRichiestePersonali() {

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
  		
		return ResponseEntity.ok(richiestaService.getRichiesteByUtenteId(user.getId()));
	}
	
	@RequestMapping(value="/protected/richiesta/utente/partecipazioni", method = RequestMethod.GET)
	public ResponseEntity<?> getPartecipazioni() {

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
  		
		return ResponseEntity.ok(richiestaService.getPartecipazioni(user.getId()));
	}

	@RequestMapping(value = "/protected/richiesta", method = RequestMethod.POST)
	public ResponseEntity<?> saveRichiesta(@RequestBody Richiesta richiesta, BindingResult bindingResult) {

		richiestaValidator.validate(richiesta, bindingResult);

		
		if(!bindingResult.hasErrors()) {
			UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();
			
			Utente user = utenteService.getByEmail(userDetails.getUsername());
			
			richiesta.setDataCreazione(ZonedDateTime.now());
			richiesta.setCreatore(user);
			
			return ResponseEntity.ok(richiestaService.save(richiesta).getId());
		}
		
		return ResponseEntity.badRequest().body(false);
	}
	
	@RequestMapping(value = "/protected/partecipa/richiesta/{id_richiesta}", method = RequestMethod.PUT)
	public ResponseEntity<?> partecipaRichiesta(@PathVariable("id_richiesta") Long id_richiesta) {

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());

		if (richiestaService.exists(id_richiesta)) {
			Richiesta r = richiestaService.getRichiesta(id_richiesta);
			
			if(r.isDisponibile() && !r.allradyContains(user) && r.getCreatore().getId() != user.getId()) {
				
				r.addPartecipante(user);
				user.addPartecipazioneAProposta(r);
				
				richiestaService.save(r);

				Notifica n = new Notifica();
				n.setDescrizione(user.getNome() + " ha accettato la tua proposta : " + r.getTitolo());
				n.setRichiesta(r);
				n.setUtente(r.getCreatore());
				n.setSoggetto(user);
				
				notificaService.save(n);
				
				return ResponseEntity.ok(true);
			}else if(r.allradyContains(user)) {
				r.removePartecipante(user);
				user.rimuoviPartecipazione(r);
				notificaService.removeByUtenteAndRichiesta(user.getId(), r.getId());
				richiestaService.save(r);
				return ResponseEntity.ok(false);
				
			}
			
		}

		return ResponseEntity.badRequest().body(false);
	}

	@RequestMapping(value = "/protected/richiesta/{id}", method = RequestMethod.DELETE)
	public ResponseEntity<?> removeRichiesta(@PathVariable("id") Long id) {

		Richiesta richiesta = richiestaService.getRichiesta(id);
		
		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());

		if (user.getId() == richiesta.getCreatore().getId()) {
			richiestaService.remove(id);
			return ResponseEntity.ok(true);
		}
		
		return ResponseEntity.badRequest().body(false);
	}

	@RequestMapping(value = "/public/richiesta/{offset}/{row_count}", method = RequestMethod.GET)
	public ResponseEntity<?> getRichiesteInRowRange(@PathVariable("offset") Long offset, @PathVariable("row_count") Long row_count) {
		return ResponseEntity.ok(richiestaService.getRichiesteInRowRange(offset, row_count));
	}
}
