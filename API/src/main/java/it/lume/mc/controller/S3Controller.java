package it.lume.mc.controller;

import java.io.ByteArrayOutputStream;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import it.lume.mc.model.Richiesta;
import it.lume.mc.model.Utente;
import it.lume.mc.service.RichiestaService;
import it.lume.mc.service.S3ServicesImpl;
import it.lume.mc.service.UtenteService;
import software.amazon.ion.Timestamp;

@RestController
public class S3Controller {

	@Autowired
	private S3ServicesImpl s3Services;

	@Autowired
	private UtenteService utenteService;
	
	@Autowired
	private RichiestaService richiestaService;
	
	private static final String PATH_IMMAGINE_UTENTE = "immagine/utente/";
	private static final String PATH_IMMAGINE_RICHIESTA = "immagine/richiesta/";
	

	private static final String RESOURCE_NAME_UTENTE = "/public/file/utente/";
	private static final String RESOURCE_NAME_RICHIESTA = "/public/file/richiesta/";
	
	private static final String HOST_NAME = "lume-api-99.herokuapp.com";
//	private static final String LOCAL_HOST_NAME = "localhost:8090";
	private static final String PROTOCOL = "https";
//	private static final String LOCAL_PROTOCOL = "http";
	
	
	public String generateLink(String resourceName, String fileName) {
		StringBuilder sb = new StringBuilder();

		sb	
			.append(PROTOCOL).append("://")
			.append(HOST_NAME)
			.append(resourceName)
			.append(fileName);
		
		
		return sb.toString();
	}
	
	public String generateFileKey(String originalFileName, Long id) {
		StringBuilder sb = new StringBuilder();

		sb	
			.append(id)
			.append("/")
			.append(Timestamp.now().hashCode())
			.append("-")
			.append(originalFileName);
		
		
		return sb.toString();
	}

	
    @RequestMapping(value = "/protected/file/upload/immagine/utente", method = RequestMethod.POST)
    public ResponseEntity<?> uploadMultipartFile(@RequestParam("file") MultipartFile file) {

		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
		
    	String fileKey = generateFileKey(file.getOriginalFilename(), user.getId());
    	
    	
    	String link = generateLink(RESOURCE_NAME_UTENTE, fileKey);
		
    	s3Services.uploadFile(PATH_IMMAGINE_UTENTE + fileKey, file);
    	user.setImmagine(link);

    	utenteService.save(user);
    	
		return ResponseEntity.ok(link);
    } 
    
    
    @RequestMapping(value = "/protected/file/upload/immagine/richiesta/{id_richiesta}", method = RequestMethod.POST)
    public ResponseEntity<?> uploadImmaginUtente(@RequestParam("file") MultipartFile file,
    								  			 @PathVariable("id_richiesta") Long id_richiesta) {
		Richiesta r = richiestaService.getRichiesta(id_richiesta);
		
		UserDetails userDetails = (UserDetails)SecurityContextHolder.getContext().getAuthentication().getPrincipal();

		Utente user = utenteService.getByEmail(userDetails.getUsername());
		
		if(!r.getCreatore().equals(user)) return ResponseEntity.badRequest().body(null);
		
		String fileKey = generateFileKey(file.getOriginalFilename(), id_richiesta);
    	
    	
    	String link = generateLink(RESOURCE_NAME_RICHIESTA, fileKey);
		
    	s3Services.uploadFile(PATH_IMMAGINE_RICHIESTA + fileKey, file);
		r.setImmagine(link);
    	
		richiestaService.save(r);

		return ResponseEntity.ok(link);
    } 
    
    /*
     * Download Files
     */
	@RequestMapping(value = RESOURCE_NAME_UTENTE + "{id_utente}/{fileName}", method = RequestMethod.GET)
	public ResponseEntity<?> getImmagineUtente(@PathVariable("fileName") String fileName,
											   @PathVariable("id_utente") Long id_utente) {
		
		ByteArrayOutputStream downloadInputStream = s3Services.downloadFile(PATH_IMMAGINE_UTENTE + id_utente + "/" + fileName);
	
		return ResponseEntity.ok()
					.contentType(contentType(fileName))
					.header(HttpHeaders.CONTENT_DISPOSITION,"attachment; filename=\"" + fileName + "\"")
					.body(downloadInputStream.toByteArray());	
	}
	
	
	
	@RequestMapping(value = RESOURCE_NAME_RICHIESTA + "{id_richiesta}/{fileName}", method = RequestMethod.GET)
	public ResponseEntity<?> getImmagineRichiesta(@PathVariable("fileName") String fileName,
												  @PathVariable("id_richiesta") Long id_richiesta) {
		
		ByteArrayOutputStream downloadInputStream = s3Services.downloadFile(PATH_IMMAGINE_RICHIESTA + id_richiesta + "/" + fileName);
		
		return ResponseEntity.ok()
					.contentType(contentType(fileName))
					.header(HttpHeaders.CONTENT_DISPOSITION,"attachment; filename=\"" + fileName + "\"")
					.body(downloadInputStream.toByteArray());	
	}
	
	private MediaType contentType(String fileName) {
		String[] arr = fileName.split("\\.");
		String type = arr[arr.length-1];
		switch(type) {
			case "txt": return MediaType.TEXT_PLAIN;
			case "png": return MediaType.IMAGE_PNG;
			case "jpg": return MediaType.IMAGE_JPEG;
			case "jpeg": return MediaType.IMAGE_JPEG;
			default: return MediaType.APPLICATION_OCTET_STREAM;
		}
	}
}
