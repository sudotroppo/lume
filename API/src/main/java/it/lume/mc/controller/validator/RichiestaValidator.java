package it.lume.mc.controller.validator;

import org.springframework.stereotype.Component;
import org.springframework.validation.Errors;
import org.springframework.validation.ValidationUtils;
import org.springframework.validation.Validator;

import it.lume.mc.model.Utente;

@Component
public class RichiestaValidator implements Validator {


	@Override
	public void validate(Object obj, Errors errors) {

		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "titolo"            , "_");
		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "descrizione"       , "_");
		ValidationUtils.rejectIfEmptyOrWhitespace(errors, "numeroPartecipanti", "_");
			
	}
	
	@Override
	public boolean supports(Class<?> clazz) {
		return Utente.class.equals(clazz);
	}

}
