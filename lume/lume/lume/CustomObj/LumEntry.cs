using System;
using Xamarin.Forms;


namespace lume
{
    public class LumEntry : Entry
    {

        //insieme delle proprietà aggiuntive della entry
        #region Bindable Properties
        public static readonly BindableProperty HighlightColorProperty = BindableProperty.Create(nameof(HighlightColor), typeof(Color), typeof(LumEntry));

        public static readonly BindableProperty HandleColorProperty = BindableProperty.Create(nameof(HandleColor), typeof(Color), typeof(LumEntry));

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(LumEntry));

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(LumEntry));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(LumEntry));

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(LumEntry));

        public static readonly BindableProperty CurveIsEnabledProperty = BindableProperty.Create(nameof(CurveIsEnabled), typeof(bool), typeof(LumEntry));
        #endregion

        //codice che converte da xaml in c# i valori delle proprietà (?)
        #region Properties

        //colore dell'evidenziatore di selezione
        public Color HighlightColor
        {
            get => (Color)GetValue(HighlightColorProperty);
            set => SetValue(HighlightColorProperty, value);
        }

        //colore dei cursori di selezione
        public Color HandleColor
        {
            get => (Color)GetValue(HandleColorProperty);
            set => SetValue(HandleColorProperty, value);
        }

        //colore dela tinta del testo
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        //colore della line esterna
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        //raggio di approccio degli spigoli
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        //raggio di approccio degli spigoli
        public float BorderWidth
        {
            get => (float)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        //raggio di approccio degli spigoli
        public bool CurveIsEnabled
        {
            get => (bool)GetValue(CurveIsEnabledProperty);
            set => SetValue(CurveIsEnabledProperty, value);
        }
        #endregion

        public LumEntry()
        {
        }
    }
}
