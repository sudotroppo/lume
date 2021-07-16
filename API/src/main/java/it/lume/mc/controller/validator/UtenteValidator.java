package it.lume.mc.controller.validator;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.validation.Errors;
import org.springframework.validation.ValidationUtils;
import org.springframework.validation.Validator;

import it.lume.mc.model.Utente;
import it.lume.mc.service.UtenteService;

@Component
public class UtenteValidator implements Validator {

	@Autowired
	private UtenteService utenteService;

	@Override
	public void validate(Object obj, Errors errors) {
		
		Utente u = (Utente)obj;

		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "password", "_");
		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "nome"    , "_");
		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "cognome" , "_");
		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "email"   , "_");
		
		if(utenteService.existsByEmail(u.getEmail())) {
			errors.reject("allRadyExists");
		}
		
	}
	@Override
	public boolean supports(Class<?> clazz) {
		return Utente.class.equals(clazz);
	}

}
