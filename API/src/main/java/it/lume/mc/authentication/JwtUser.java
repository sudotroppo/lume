package it.lume.mc.authentication;

import java.util.Collection;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import com.fasterxml.jackson.annotation.JsonIgnore;

public class JwtUser implements UserDetails {

	private static final long serialVersionUID = -387890284405311400L;

	private final String username;
    
	private final String password;
    
	private final Collection<? extends GrantedAuthority> authorities;
    
	private final boolean enabled;

    public JwtUser(String username, String password, Collection<? extends GrantedAuthority> authorities, boolean enabled) {
    	
        this.username = username;
        
        this.password = password;
        
        this.authorities = authorities;
        
        this.enabled = enabled;
    }


    @Override
    public String getUsername() {
        return username;
    }

    @JsonIgnore
    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    @JsonIgnore
    @Override
    public boolean isAccountNonLocked() {
        return true;
    }

    @JsonIgnore
    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }


    @JsonIgnore
    @Override
    public String getPassword() {
        return password;
    }

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return authorities;
    }

    @Override
    public boolean isEnabled() {
        return enabled;
    }

}

