package it.lume.mc.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import it.lume.mc.model.Notifica;
import it.lume.mc.model.Utente;
import it.lume.mc.service.NotificaService;
import it.lume.mc.service.UtenteService;

@RestController
@RequestMapping("/protected/notifica")
public class NotificaController {

	@Autowired
	private NotificaService notificaService;

	@Autowired
	private UtenteService utenteService;

	@RequestMapping(value = "/utente", method = RequestMethod.GET)
	public ResponseEntity<?> getNotificaByUtente() {
		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
		
		return ResponseEntity.ok(notificaService.getNotificheByUtente(user.getId()));
	}

	@RequestMapping(value = "/richiesta/{id}", method = RequestMethod.GET)
	public ResponseEntity<?> getNotificaByRichiesta(@PathVariable("id") Long richiesta_id) {
		return ResponseEntity.ok(notificaService.getNotificheByRichiesta(richiesta_id));
	}

	@RequestMapping(value = "", method = RequestMethod.POST)
	public ResponseEntity<?> saveNotifica(@ModelAttribute("notifica") Notifica notifica) {
		return ResponseEntity.ok(notificaService.save(notifica));
	}

//	@RequestMapping(value = "/{id}", method = RequestMethod.DELETE)
//	public void removeNotifica(@PathVariable("id") Long id) {
//		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();
//
//		Utente user = utenteService.getUtenteByEmail(userDetails.getUsername());
//		
//		notificaService.remove(id);
//	}
}
