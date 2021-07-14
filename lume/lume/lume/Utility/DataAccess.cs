using System;
using System.Text.Json;
using RestSharp;
using lume.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;
using System.IO;

namespace lume.Utility
{
    public class DataAccess
    {

        public static void RefreshToken()
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/refresh-token", Method.GET)
            {
                Timeout = App.requestTimeout
            };

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

        public static Boolean PartecipaAProposta(long id_richiesta)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/partecipa/richiesta/{id_richiesta}", Method.PUT)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            Boolean result = JsonSerializer.Deserialize<Boolean>(response.Content);
            return result;
        }

        public static TokenResponse GetToken(string email, string password)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/public/login", Method.POST)
            {
                Timeout = App.requestTimeout
            };

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
            var request = new RestRequest("/protected/utente", Method.GET)
            {
                Timeout = App.requestTimeout
            };

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
            request.Timeout = App.requestTimeout;

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
            var request = new RestRequest("/protected/utente", Method.PUT)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            request.AddQueryParameter("nome", utente.nome);
            request.AddQueryParameter("cognome", utente.cognome);
            request.AddQueryParameter("email", utente.email);
            request.AddQueryParameter("citta", utente.citta);
            request.AddQueryParameter("telefono", utente.telefono);

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
            var request = new RestRequest($"/public/richiesta/{richiestaId}", Method.GET)
            {
                Timeout = App.requestTimeout
            };

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            Richiesta richiesta = JsonSerializer.Deserialize<Richiesta>(response.Content);

            return richiesta;
        }

        public static void DeleteRichiesta(long id_richiesta)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/richiesta/{id_richiesta}", Method.DELETE)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Console.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

        }

        public static List<Richiesta> GetAllRichieste()
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/public/richiesta", Method.GET)
            {
                Timeout = App.requestTimeout
            };

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Richiesta> richiesta = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richiesta;
        }

        public static long NewRichiesta(Richiesta richiesta)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/richiesta", Method.POST)
            {
                Timeout = App.requestTimeout
            };

            request.AddJsonBody(
                new
                {
                    titolo = richiesta.titolo,
                    descrizione = richiesta.descrizione,
                    numeroPartecipanti = richiesta.numeroPartecipanti
                });

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);



            Console.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);


            long id_richiesta = JsonSerializer.Deserialize<long>(response.Content);

            return id_richiesta;
        }

        internal static List<Notifica> GetNotificheByUtente()
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/notifica/utente", Method.GET)
            {
                Timeout = App.requestTimeout
            };

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
            var request = new RestRequest($"/protected/richiesta/{offset}/{row_count}", Method.GET)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Richiesta> richieste = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richieste;
        }

        public static List<Richiesta> GetRichiesteUtente()
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/richiesta/utente", Method.GET)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Richiesta> richieste = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richieste;
        }

        public static List<Richiesta> GetPartecipazioniUtente()
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/richiesta/utente/partecipazioni", Method.GET)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            List<Richiesta> richieste = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richieste;
        }

        public static void UploadUserImage(Stream stream, string fileName)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/protected/file/upload/immagine/utente", Method.POST)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            request.AddFile("file", ReadFully(stream), fileName, "multipart/form-data");

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            client.Execute(request);

        }

        public static void UploadRequestImage(Stream stream, string fileName, long id_richiesta)
        {
            DataAccess.RefreshToken();

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/protected/file/upload/immagine/richiesta/{id_richiesta}", Method.POST)
            {
                Timeout = App.requestTimeout
            };

            request.AddHeader(Constants.AUTHENTICATION_HEADER, App.token);

            request.AddFile("file", ReadFully(stream), fileName, "multipart/form-data");

            Debug.WriteLine($"--------{client.BuildUri(request)}--------");

            client.Execute(request);

        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
