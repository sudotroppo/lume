using lume.CustomObj;
using lume.Domain;
using lume.Templates;
using lume.Utility;
using lume.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContentTemplatedView = lume.Templates.ContentTemplatedView;

namespace lume.Pages
{
    public partial class FillRequestPage : ContentTemplatedView
    {
        public FillRequestPage(Navigator navigator) : base(navigator)
        {
            InitializeComponent();

        }


    }
}