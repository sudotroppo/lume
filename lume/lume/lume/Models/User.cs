using SQLite;
using Xamarin.Forms;

namespace lume.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public Image Image { set; get; }

        public User() : this("None", "None", "None", "None", new Image()) { }

        public User(string Name, string Surname, string Address, string Password, Image Image)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Password = Password;
            this.Address = Address;
            this.Image = Image;
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

    }
}
