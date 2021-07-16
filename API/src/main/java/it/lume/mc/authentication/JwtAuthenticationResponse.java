package it.lume.mc.authentication;

import java.io.Serializable;
import java.util.Collection;

import org.springframework.security.core.GrantedAuthority;

import lombok.AllArgsConstructor;
import lombok.Data;

@AllArgsConstructor
public @Data class JwtAuthenticationResponse implements Serializable {


    /**
	 * 
	 */
	private static final long serialVersionUID = -6890869487578157670L;
	
	private final String email;
	
    Collection<? extends GrantedAuthority> authorities;
    
    private String token;

}
