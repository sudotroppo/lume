package it.lume.mc.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import it.lume.mc.controller.validator.UtenteValidator;
import it.lume.mc.model.Utente;
import it.lume.mc.service.UtenteService;

@RestController
public class UtenteController {

	@Autowired
	private UtenteService utenteService;

	@Autowired
	private UtenteValidator utenteValidator;

	@RequestMapping(value = "/protected/utente", method = RequestMethod.GET)
	public ResponseEntity<?> getUtenteByEmail(@ModelAttribute("email") String email) {

		if(utenteService.existsByEmail(email)) {
			return ResponseEntity.ok(utenteService.getByEmail(email));
		}
		return ResponseEntity.badRequest().body(null);
	}
	
	@RequestMapping(value = "/public/utente/exists", method = RequestMethod.GET)
	public ResponseEntity<?> existsByEmail(@ModelAttribute("email") String email) {

		return ResponseEntity.ok(utenteService.existsByEmail(email));
	}

	@RequestMapping(value = "/public/regist", method = RequestMethod.POST)
	public ResponseEntity<?> registraUtente(@ModelAttribute("utente") Utente utente, BindingResult bindingResult) {

		utenteValidator.validate(utente, bindingResult);

		if(!bindingResult.hasErrors()) {
			utente.setEnabled(true);
			utente.setRuolo(Utente.USER_ROLE);
			
			utenteService.registra(utente);
			
			return ResponseEntity.ok(true);
		}
		return ResponseEntity.badRequest().body(false);
	}


	@RequestMapping(value = "/protected/utente", method = RequestMethod.PUT)
	public ResponseEntity<?> updateUtente(@ModelAttribute("utente") Utente utente) {
		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());

		utenteService.update(utente, user);
		return ResponseEntity.ok(true);
	}

	@RequestMapping(value = "/protected/utente", method = RequestMethod.DELETE)
	public ResponseEntity<?> removeUtente() {

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
		utenteService.remove(user.getId());
		
		return ResponseEntity.ok(true);
	}
	
	@RequestMapping(value = "/protected/utente/check", method = RequestMethod.GET)
	public ResponseEntity<?> checkPassword(@ModelAttribute("password") String pwd){

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
		
		boolean result = utenteService.checkPassword(pwd, user.getPassword());
		
		return ResponseEntity.ok(result);
	}
	
	@RequestMapping(value = "/protected/utente/change/password", method = RequestMethod.PUT)
	public ResponseEntity<?> changePassword(@ModelAttribute("password") String pwd){

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());

		utenteService.setPassword(pwd, user);
		return ResponseEntity.ok(true);
	}
}
