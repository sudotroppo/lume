using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace lume.Templates
{
    public partial class SettingsTemplate : ContentView
    {

        public static readonly BindableProperty OnSettingCommandProperty =
            BindableProperty.Create(
                nameof(OnSettingCommand),
                typeof(Command),
                typeof(SettingsTemplate));

        public static readonly BindableProperty SettingTextProperty =
            BindableProperty.Create(
                nameof(SettingText),
                typeof(string),
                typeof(SettingsTemplate),
                "");

        public static readonly BindableProperty IsButtonVisibileProperty =
            BindableProperty.Create(
                nameof(IsButtonVisibile),
                typeof(bool),
                typeof(SettingsTemplate),
                true);

        public static readonly BindableProperty OnSettingCommandParameterProperty =
            BindableProperty.Create(
                nameof(OnSettingCommandParameter),
                typeof(object),
                typeof(SettingsTemplate),
                null);

        public string SettingText
        {
            get => (string)GetValue(SettingTextProperty);
            set => SetValue(SettingTextProperty, value);
        }

        public Command OnSettingCommand
        {
            get => (Command)GetValue(OnSettingCommandProperty);
            set => SetValue(OnSettingCommandProperty, value);
        }

        public bool IsButtonVisibile
        {
            get => (bool)GetValue(IsButtonVisibileProperty);
            set => SetValue(IsButtonVisibileProperty, value);
        }

        public object OnSettingCommandParameter
        {
            get => GetValue(OnSettingCommandParameterProperty);
            set => SetValue(OnSettingCommandParameterProperty, value);
        }



        public SettingsTemplate()
        {
            InitializeComponent();
            BindingContext = this;


        }

    }
}
