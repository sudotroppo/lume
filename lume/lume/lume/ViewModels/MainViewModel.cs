using lume.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using lume.Domain;
using Xamarin.Forms;

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
                UriImageSources = new UriImageSource
                {
                    Uri = new Uri("https://www.bellacanzone.it/wp-content/uploads/2019/12/Domenico-Bini-840x420.jpg"),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(24,0,0),
                }
            };

            _Posts = new List<Post>
            {
               new Post
               {
                   Owner = CurrentUser,
                   Description = "Aiutatemi a pulire il culetto!!1!11!",
                   Number = 1
               },
               new Post
               {
                   Owner = CurrentUser,
                   Description = "Il Vulcanoooooo",
                   Number = 0
               },
               new Post
               {
                   Owner = CurrentUser,
                   Description = "Ha eruttatoooo",
                   Number = 100
               },
               new Post
               {
                   Owner = CurrentUser,
                   Description = "Ueeeeee",
                   Number = 10
               }

            };
        }
    }
}
