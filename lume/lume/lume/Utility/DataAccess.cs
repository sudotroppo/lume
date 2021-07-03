using System;
using System.Text.Json;
using RestSharp;
using lume.Domain;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;

namespace lume.Utility
{
    public class DataAccess
    {

        //public string GetQueryString(object obj)
        //{
        //    var properties = from p in obj.GetType().GetProperties()
        //                     where p.GetValue(obj, null) != null
        //                     select p.Name + "={" + p.Name + "}";

        //    return String.Join("&", properties.ToArray());
        //}

        //private static void SetParameter(object obj, string suffisso, RestRequest request)
        //{
        //    foreach (PropertyInfo p in obj.GetType().GetProperties())
        //    {
        //        var value = p.GetValue(obj, null);

        //        if (value != null)
        //        {
        //            if (value.GetType().Namespace.Contains("lume."))
        //            {
        //                SetParameter(value, suffisso + value.GetType().Name.ToLower() + ".", request);
        //            }

        //            request.AddParameter(suffisso + p.Name.ToLower(), value.ToString(), ParameterType.UrlSegment); 
        //        }
        //    }
        //}

        public static Utente GetUtenteByEmail(string email)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/utente",Method.GET);

            request.AddParameter("email", email);

            Console.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

            Utente utente = JsonSerializer.Deserialize<Utente>(response.Content);

            return utente;
        }

        public static void NewUtente(Utente utente)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/utente", Method.POST);

            request.AddQueryParameter("nome", utente.nome);
            request.AddQueryParameter("cognome", utente.cognome);
            request.AddQueryParameter("email", utente.email);
            request.AddQueryParameter("telefono", utente.telefono);
            request.AddQueryParameter("immagine", utente.immagine);

            IRestResponse response = client.Execute(request);
        }

        public static void UpdateUtente(Utente utente)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/utente", Method.PUT);

            request.AddQueryParameter("id", utente.id.ToString());
            request.AddQueryParameter("nome", utente.nome);
            request.AddQueryParameter("cognome", utente.cognome);
            request.AddQueryParameter("email", utente.email);
            request.AddQueryParameter("telefono", utente.telefono);
            request.AddQueryParameter("immagine", utente.immagine);

            IRestResponse response = client.Execute(request);
        }

        public static Richiesta GetRichiestaById(long richiestaId)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/richiesta/{richiestaId}", Method.GET);

            IRestResponse response = client.Execute(request);

            Richiesta richiesta = JsonSerializer.Deserialize<Richiesta>(response.Content);

            return richiesta;
        }

        public static List<Richiesta> GetAllRichieste()
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/richiesta", Method.GET);

            IRestResponse response = client.Execute(request);

            List<Richiesta> richiesta = JsonSerializer.Deserialize<List<Richiesta>>(response.Content);

            return richiesta;
        }

        public static void NewRichiesta(Richiesta richiesta)
        {
            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest("/richiesta", Method.POST);

            request.AddQueryParameter("titolo", richiesta.titolo);
            request.AddQueryParameter("descrizione", richiesta.descrizione);
            request.AddQueryParameter("numeroPartecipanti", richiesta.numeroPartecipanti.ToString());
            request.AddQueryParameter("creatore.id", richiesta.creatore.id.ToString());

            Console.WriteLine($"--------{client.BuildUri(request)}--------");

            IRestResponse response = client.Execute(request);

        }

        internal static List<Notifica> GetNotificheByUtente(long utente_id)
        {

            var client = new RestSharp.RestClient(Constants.API_ENDPOINT);
            var request = new RestRequest($"/notifica/utente/{utente_id}", Method.GET);

            IRestResponse response = client.Execute(request);

            List<Notifica> notifiche = JsonSerializer.Deserialize<List<Notifica>>(response.Content);

            return notifiche;
        }
    }
}
