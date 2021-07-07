using System;
using System.Text.Json;
using RestSharp;
using lume.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;

namespace lume.Utility
{
    public class DataAccess
    {

        public static void RefreshToken()
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/refresh-token", Method.GET);

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("request unuthorized");
            }

            TokenResponse token = JsonSerializer.Deserialize<TokenResponse>(response.Content);

            SecureStorage.SetAsync("token", token.token);
        }

        public static TokenResponse GetToken(string email, string password)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/public/login", Method.POST);

            request.AddJsonBody(
                new {
                        email = email,
                        password = password
                    });

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");
            IRestResponse response = client.Execute(request);
            Debug.WriteLine($"--------{response.Content}--------");

            TokenResponse token = JsonSerializer.Deserialize<TokenResponse>(response.Content);

            return token;
        }

        public static Utente GetUtenteByEmail(string email)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/utente",Method.GET);

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            request.AddParameter("email", email);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            Utente utente = JsonSerializer.Deserialize<Utente>(response.Content);

            Debug.WriteLine($"--------id = {utente.id}, nome = {utente.nome}--------");

            return utente;
        }

        public static void NewUtente(Utente utente)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/public/regist", Method.POST);

            request.AddQueryParameter("nome", utente.nome);
            request.AddQueryParameter("cognome", utente.cognome);
            request.AddQueryParameter("email", utente.email);
            request.AddQueryParameter("telefono", utente.telefono);
            request.AddQueryParameter("immagine", utente.immagine);
            request.AddQueryParameter("password", utente.password);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);
        }

        public static void UpdateUtente(Utente utente)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/utente", Method.PUT);

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            request.AddQueryParameter("id", utente.id.ToString());
            request.AddQueryParameter("nome", utente.nome);
            request.AddQueryParameter("cognome", utente.cognome);
            request.AddQueryParameter("email", utente.email);
            request.AddQueryParameter("telefono", utente.telefono);
            request.AddQueryParameter("immagine", utente.immagine);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            if (!App.utente.email.Equals(utente.email))
            {
                SecureStorage.SetAsync("email", utente.email);

            }
        }

        public static Richiesta GetRichiestaById(long richiestaId)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/public/richiesta/{richiestaId}", Method.GET);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            Richiesta richiesta = JsonSerializer.Deserialize<Richiesta>(response.Content);

            return richiesta;
        }

        public static List<Richiesta> GetAllRichieste()
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/public/richiesta", Method.GET);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Richiesta> richiesta = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richiesta;
        }

        public static void NewRichiesta(Richiesta richiesta)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/richiesta", Method.POST);

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            request.AddQueryParameter("titolo", richiesta.titolo);
            request.AddQueryParameter("descrizione", richiesta.descrizione);
            request.AddQueryParameter("numeroPartecipanti", richiesta.numeroPartecipanti.ToString());
            request.AddQueryParameter("creatore.id", richiesta.creatore.id.ToString());

            Console.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

        }

        internal static List<Notifica> GetNotificheByUtente(long utente_id)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/notifica/utente/{utente_id}", Method.GET);

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Notifica> notifiche = JsonSerializer.Deserialize<List<Notifica>>(response.Content);

            return notifiche;
        }

        public static List<Richiesta> GetRichiesteInRowRange(long offset, long row_count)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/richiesta/{offset}/{row_count}", Method.GET);

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Richiesta> richieste = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richieste;
        }
    }
}
