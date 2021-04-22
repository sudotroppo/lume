using Xamarin.Forms;

namespace lume.Domain
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public UriImageSource Image 
        { 
            set; 
            get; 
        }


        public override string ToString()
        {
            return Name + " " + Surname;
        }

    }
}
