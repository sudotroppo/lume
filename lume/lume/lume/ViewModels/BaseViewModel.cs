using lume.ViewModels;
using Xamarin.Forms;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections.Generic;
using lume.CustomObj;
using lume.Pages;

namespace lume.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            Debug.WriteLine($"propertyName = {propertyName}");
            OnPropertyChanged(propertyName);
            return true;
        }

        public Navigator navigator { get => ((Application.Current.MainPage as CustomNavigationPage).CurrentPage as MainPage).navigator; }


    }
}