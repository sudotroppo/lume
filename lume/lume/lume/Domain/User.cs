using Xamarin.Forms;

namespace lume.Domain
{
    public class Utente
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public string Indirizzo { get; set; }

        public string Password { get; set; }

        public UriImageSource Immagine 
        { 
            set; 
            get; 
        }


        public override string ToString()
        {
            return Nome + " " + Cognome;
        }

    }
}
