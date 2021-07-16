package it.lume.mc.authentication;

import java.io.Serializable;

import lombok.AllArgsConstructor;
import lombok.Data;

@AllArgsConstructor
public @Data class  JwtAuthenticationRequest implements Serializable {

    private static final long serialVersionUID = -8445943548965154778L;

    private String email;
    
    private String password;

    public JwtAuthenticationRequest() {
        super();
    }

}