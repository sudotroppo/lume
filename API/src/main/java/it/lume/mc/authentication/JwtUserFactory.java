package it.lume.mc.authentication;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;

import it.lume.mc.model.Utente;

public final class JwtUserFactory {

    private JwtUserFactory() {
    }

    public static JwtUser create(Utente user) {
    	List<String> ruoli = new ArrayList<>();
    	ruoli.add(user.getRuolo());
    	
        return new JwtUser(
                user.getEmail(),
                user.getPassword(),
                mapToGrantedAuthorities(ruoli),
                user.getEnabled()
        );
    }

    private static List<GrantedAuthority> mapToGrantedAuthorities(List<String> authorities) {
        return authorities.stream()
                .map(authority -> new SimpleGrantedAuthority(authority))
                .collect(Collectors.toList());
    }
}