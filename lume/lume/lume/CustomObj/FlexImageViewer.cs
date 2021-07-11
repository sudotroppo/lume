using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lume.CustomObj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class FlexImageViewer : FlexLayout
    {
        private static BindableProperty ItemSourceProperty =
            BindableProperty.Create(
                                    propertyName:  nameof(ItemsSource),
                                    returnType: typeof(List<string>),
                                    declaringType: typeof(FlexImageViewer),
                                    defaultValue: new List<string>(),
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: ItemSourcePropertyChanged
                );

        public List<string> ItemsSource { set; get; }

        private static void ItemSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FlexImageViewer)bindable;
            control.ItemsSource = (List<string>)newValue;
            foreach(string img in control.ItemsSource)
            {
                control.Children.Add(new Image
                {
                    Source = ImageSource.FromUri(new Uri(img))
                });
            }
        }

        public FlexImageViewer()
        {
            JustifyContent = FlexJustify.Center;
            AlignItems = FlexAlignItems.Center;
            FlowDirection = FlowDirection.LeftToRight;
            Direction = FlexDirection.Row;
        }
    }
}
