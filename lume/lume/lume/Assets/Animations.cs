using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lume.Assets
{
    public static class Animations
    {
        //funzione che restituisce una animazione che trasla un elemento View v lungo gli assi x ed y di dx e dy
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

        //funzione che restituisce una animazione che trasla un elemento View v lungo l'asse x di dx 
        public static Animation SlideOfX(View v, double dx, Easing easing)
        {
            double dxi = v.TranslationX;

            return new Animation(c =>
            {
                v.TranslationX = dxi + c * dx;
            },
            0, 1, easing ?? Easing.Linear);
        }

        //funzione che restituisce una animazione che trasla un elemento View v lungo l'asse y di dy
        public static Animation SlideOfY(View v, double dy, Easing easing)
        {
            double dyi = v.TranslationY;

            return new Animation(c =>
            {
                v.TranslationY = dyi + c * dy;
            },
            0, 1, easing ?? Easing.Linear);
        }

        //funzione che restituisce una animazione che scala un elemento View a scaleX e scaleY
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

        //funzione che restituisce una animazione che sfuma il colore della view v al colorTarghet
        public static Animation ChangeColor(View v, BindableProperty colorProperty, Color targhetColor, Easing easing)
        {
            Color initialColor = (Color)v.GetValue(colorProperty);

            //valori iniziali
            double ir = initialColor.R;
            double ig = initialColor.G;
            double ib = initialColor.B;
            double ia = initialColor.A;

            //valori finali
            double er = targhetColor.R;
            double eg = targhetColor.G;
            double eb = targhetColor.B;
            double ea = targhetColor.A;

            //delta valori
            double dr = er - ir;
            double dg = eg - ig;
            double db = eb - ib;
            double da = ea - ia;

            return new Animation(c =>
            {
                v.SetValue(colorProperty, Color.FromRgba(c * dr, c * dg, c * db, c * da));
            },
            0, 1, easing ?? Easing.Linear);
        }

        //funzione che conta tutti i figli di un layout
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

        //funzione che restituisce una animazione che viene applicata a tutti i figli di un layout
        public static Animation CascadeToTheChildren(Layout<View> layout, Func<View, Animation> func)
        {
            return CascadeToTheChildrenCall(layout, func, 0, 1f / CountChildren(layout, 0));
        }

        private static Animation CascadeToTheChildrenCall(Layout<View> layout, Func<View, Animation> func, int index, double slot)
        {
            Animation a = new Animation();
            foreach (View child in layout.Children)
            {
                if (child is Layout<View> cl)
                {
                    a.Add(slot * index, slot * (index + 1), CascadeToTheChildrenCall(cl, func, index, slot));
                }
                else
                {
                    a.Add(slot * index, slot * (index + 1), func.Invoke(child));
                }
                index++;
            }
            return a;
        }

        //funzione che restituisce una animazione
        public static Animation FadeTo(View v, double opacity, Easing easing)
        {
            double initialOpacity = v.Opacity;
            double deltaOpacity = opacity - initialOpacity;
            return new Animation(c =>
            {
                v.Opacity = initialOpacity + (c * deltaOpacity);
            });
        }

        //funzione che restituisce una animazione che effettua una rotazione relativa dell'elemento View v
        public static Animation RelativeRotation(View v, double degrees, Easing easing)
        {
            double initialDegrees = v.Rotation;

            return new Animation((c) =>
            {
                v.Rotation = initialDegrees + c * degrees;
            }, 0, 1, easing ?? Easing.Linear);
        }

        //funzione che restituisce una animazione che effettua una rotazione di un elemento View attorno ad un punto p
        public static Animation RotateAround(View v, Point p, double degrees, Easing easing)
        {
            double xi = v.X;
            double yi = v.Y;

            return new Animation((c) =>
            {
                v.TranslationX = p.X + Math.Sin(c * degrees);
                v.TranslationY = p.Y + Math.Cos((c - 1) * degrees);
            }, 0, 1, easing ?? Easing.Linear);
        }

        //funzione che calacola la posizione X assoluta di un elemento nella pagina
        public static double getAbsoluteYPosition(View v)
        {
            var y = v.Y;
            var parent = v.Parent as View;
            while (parent != null)
            {
                y += parent.Y + parent.TranslationY;
                parent = parent.Parent as View;
            }
            return y;
        }

        //funzione che calacola la posizione Y assoluta di un elemento nella pagina
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

        public static Task ShakeAnimate(View v)
        {
            return Task.Run(async () =>
            {
                uint timeout = 50;

                await v.TranslateTo(-15, 0, timeout);

                await v.TranslateTo(15, 0, timeout);

                await v.TranslateTo(-10, 0, timeout);

                await v.TranslateTo(10, 0, timeout);

                await v.TranslateTo(-5, 0, timeout);

                await v.TranslateTo(5, 0, timeout);

                v.TranslationX = 0;
            });
        }
        
    }
}
