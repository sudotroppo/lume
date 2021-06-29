using System;
using System.Collections.Generic;

namespace lume.Domain
{

    public class Lume
    {
        public Dictionary<long, Richiesta> richiesteMap { set; get; }

        public List<Richiesta> richiesteList { set; get; }

        public List<Utente> utenti { set; get; }

        private static Lume lume;

        private Lume()
        {
            richiesteList = new List<Richiesta>();
            richiesteMap = new Dictionary<long, Richiesta>();
        }

        public static Lume getIstance()
        {
            if(lume == null)
            {
                lume = new Lume(); 
            }

            return lume;
        }


        public Richiesta getRichiestaById(long richiestaId)
        {
            return richiesteMap[richiestaId];
        }

        public void addRichiesta(Richiesta r)
        {
            richiesteList.Add(r);
        }
    }
}