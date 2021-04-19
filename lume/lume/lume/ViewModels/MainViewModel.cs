using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;

namespace lume.ViewModels
{
    
    class MainViewModel : BaseViewModel
    {
        List<Post> _Posts;

        public List<Post> Posts
        {
            get { return _Posts; }
            set
            {
                _Posts = value;
                OnPropertyChanged();
            }
        }

        User _CurrentUser;

        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _CurrentUser = new User
            {
                Address = "domenicobini@pazzo.sgravato.it",
                Name = "Domenico",
                Surname = "Bini",
                Image = "https://www.bellacanzone.it/wp-content/uploads/2019/12/Domenico-Bini-840x420.jpg",
            };

            _Posts = new List<Post>
            {
                new Post
                {
                    Description = "Prova 1234",
                    UserFullName = "Domenico Bini",
                    ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHfpyHqcRq4pw/company-logo_200_200/0/1519952614888?e=2159024400&v=beta&t=XAgsNTbzezRZ_Wsbt9M73iX5qWibWFXtaKfkur61w5A",
                    Number = 3,
                },
                new Post
                {
                    Description = "Prova 1234",
                    UserFullName = "Domenico Bini",
                    ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHfpyHqcRq4pw/company-logo_200_200/0/1519952614888?e=2159024400&v=beta&t=XAgsNTbzezRZ_Wsbt9M73iX5qWibWFXtaKfkur61w5A",
                    Number = 3,
                },
                new Post
                {
                    Description = "Prova 1234",
                    UserFullName = "Domenico Bini",
                    ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHfpyHqcRq4pw/company-logo_200_200/0/1519952614888?e=2159024400&v=beta&t=XAgsNTbzezRZ_Wsbt9M73iX5qWibWFXtaKfkur61w5A",
                    Number = 3,
                },
                new Post
                {
                    Description = "Prova 1234",
                    UserFullName = "Domenico Bini",
                    ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHfpyHqcRq4pw/company-logo_200_200/0/1519952614888?e=2159024400&v=beta&t=XAgsNTbzezRZ_Wsbt9M73iX5qWibWFXtaKfkur61w5A",
                    Number = 3,
                },
                new Post
                {
                    Description = "Prova 1234",
                    UserFullName = "Domenico Bini",
                    ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHfpyHqcRq4pw/company-logo_200_200/0/1519952614888?e=2159024400&v=beta&t=XAgsNTbzezRZ_Wsbt9M73iX5qWibWFXtaKfkur61w5A",
                    Number = 3,
                },
                new Post
                {
                    Description = "Prova 1234",
                    UserFullName = "Domenico Bini",
                    ImageUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHfpyHqcRq4pw/company-logo_200_200/0/1519952614888?e=2159024400&v=beta&t=XAgsNTbzezRZ_Wsbt9M73iX5qWibWFXtaKfkur61w5A",
                    Number = 3,
                }
            };
        }
    }
}
