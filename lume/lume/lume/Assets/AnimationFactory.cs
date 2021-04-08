using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace lume.Assets
{
    public static class AnimationFactory
    {
        public static Animation SlideOf(View v, double dx, double dy, Easing easing)
        {
            double dxi = v.TranslationX;
            double dyi = v.TranslationY;

            return new Animation(c => 
            {
                v.TranslationX = dxi + c * dx;
                v.TranslationY = dyi + c * dy;
            },
            0, 1, easing ?? Easing.Linear);
        }
        public static Animation SlideOfX(View v, double dx, Easing easing)
        {
            double dxi = v.TranslationX;

            return new Animation(c =>
            {
                v.TranslationX = dxi + c * dx;
            },
            0, 1, easing ?? Easing.Linear);
        }
        public static Animation SlideOfY(View v, double dy, Easing easing)
        {
            double dyi = v.TranslationY;

            return new Animation(c =>
            {
                v.TranslationY = dyi + c * dy;
            },
            0, 1, easing ?? Easing.Linear);
        }
        public static Animation ScaleTo(View v, double scaleY, double scaleX, Easing easing)
        {
            double yi = v.ScaleY;
            double xi = v.ScaleX;

            double dy = scaleY - yi;
            double dx = scaleX - xi;
            
            return new Animation(c =>
            {
                v.ScaleY = yi + c * dy;
                v.ScaleX = xi + c * dx;
            },
            0, 1, easing ?? Easing.Linear);
        }

        public static Animation Resize(View v, double width, double heigth, Easing easing)
        {
            double wi = v.Width;
            double hi = v.Height;

            double dw = width - wi;
            double dh = heigth - hi;

            return new Animation(c =>
            {
                v.WidthRequest = wi + c * dw;
                v.HeightRequest = hi + c * dh;
            },
            0, 1, easing ?? Easing.Linear);
        }

        public static Animation ChangeColor(View v, Color colorTarghet, Easing easing) 
        {
            return new Animation(c =>
            {

            },
            0, 1, easing ?? Easing.Linear);
        }
        private static int CountChildren(Layout<View> layout, int prev)
        {
            foreach (View child in layout.Children)
            {
                if (child is Layout<View> cl)
                {
                    prev = CountChildren(cl, prev);
                }
                else
                {
                    prev++;
                }
            }

            return prev;
        }
        public static Animation CascadeToTheChildren(Layout<View> layout, Func<View,Animation> func)
        {
            return CascadeToTheChildrenCall(layout, func, 0, 1f/CountChildren(layout, 0));
        }

        private static Animation CascadeToTheChildrenCall(Layout<View> layout, Func<View, Animation> func, int index, double slot)
        {
            Animation a = new Animation();
            foreach(View child in layout.Children)
            {
                if(child is Layout<View> cl)
                {
                    a.Add(slot * index, slot * (index + 1), CascadeToTheChildrenCall(cl, func, index, slot));
                }
                else
                {
                    a.Add(slot*index, slot*(index + 1), func.Invoke(child));
                }
                index++;
            }
            return a;
        }

        public static Animation FadeTo(View v, double opacity, Easing easing)
        {
            double initialOpacity = v.Opacity;
            double deltaOpacity = opacity - initialOpacity;
            return new Animation(c =>
            {
                v.Opacity = initialOpacity + (c * deltaOpacity);
            });
        }

        public static double getAbsoluteYPosition(View v)
        {
            var y = v.Y;
            var parent = v.Parent as View;
            while (parent != null) { 
                y += parent.Y + parent.TranslationY; 
                parent = parent.Parent as View; 
            }
            return y;
        }

        public static double getAbsoluteXPosition(View v)
        {
            var x = v.X;
            var parent = v.Parent as View;
            while (parent != null)
            {
                x += parent.X + parent.TranslationX; 
                parent = parent.Parent as View;
            }
            return x;
        }
    }
}
