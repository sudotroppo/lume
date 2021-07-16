package it.lume.mc.configuration;


import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
 
import com.amazonaws.auth.AWSStaticCredentialsProvider;
import com.amazonaws.auth.BasicAWSCredentials;
import com.amazonaws.regions.Regions;
import com.amazonaws.services.s3.AmazonS3;
import com.amazonaws.services.s3.AmazonS3ClientBuilder;

@Configuration
public class S3Config {
 
    // Access key id will be read from the application.properties file during the application intialization.
    @Value("${aws.access_key_id}")
    private String accessKeyId;
    
    // Secret access key will be read from the application.properties file during the application intialization.
    @Value("${aws.secret_access_key}")
    private String secretAccessKey;
    
    // Region will be read from the application.properties file  during the application intialization.
    @Value("${aws.s3.region}")
    private String region;
 
    @Bean
	public AmazonS3 amazonS3() {
		
		BasicAWSCredentials awsCreds = new BasicAWSCredentials(accessKeyId, secretAccessKey);
		
		AmazonS3 s3Client = AmazonS3ClientBuilder.standard()
								.withRegion(Regions.fromName(region))
		                        .withCredentials(new AWSStaticCredentialsProvider(awsCreds))
		                        .build();
		
		return s3Client;
	}
}