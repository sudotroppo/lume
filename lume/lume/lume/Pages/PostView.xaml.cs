using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using lume.Models;
using Xamarin.Forms;

namespace lume.Pages
{
    public partial class PostView : ContentView
    {

        public PostView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
