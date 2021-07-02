using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using lume.Utility;

namespace lume.Domain
{

    public class Lume
    {


        public List<Richiesta> richieste
        {
            set { }
            get { return DataAccess.GetAllRichieste(); }
        }

        public List<Utente> utenti { set; get; }

        private static Lume lume;

        private Lume()
        {
            richieste = new List<Richiesta>();
        }

        public static Lume getIstance()
        {
            if(lume == null)
            {
                lume = new Lume(); 
            }

            return lume;
        }
    }
}