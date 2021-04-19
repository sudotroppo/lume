using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;

namespace lume.ViewModels
{
    
    class MainViewModel
    {
        public List<Post> Posts { set; get; }

        public MainViewModel()
        {
            Posts = new List<Post>
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
