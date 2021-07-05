using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using lume.Domain;
using lume.Utility;
using Xamarin.Forms;

namespace lume.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<Richiesta> _Posts;


        public List<Richiesta> Posts
        {
            get { return _Posts; }

            set
            {
                _Posts = value;
                OnPropertyChanged();
            }
        }


        public void Refresh()
        {
            Posts = DataAccess.GetAllRichieste();
        }

        //public void IncrementPosts(long offset, long row_count)
        //{
        //    Posts.AddRange(DataAccess.GetRichiesteInRowRange(offset, row_count));

        //    OnPropertyChanged();
        //}

        public HomeViewModel()
        {
            Task.Run(() =>
            {
                if (Posts == null || Posts.Count == 0)
                {
                    Posts = DataAccess.GetAllRichieste();
                }
            });
        }
    }
}
