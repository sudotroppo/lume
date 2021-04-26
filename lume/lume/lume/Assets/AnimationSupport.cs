using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace lume.Assets
{
    public class AnimationSupport
    {
        private readonly Dictionary<View, SettingDictionary> settings;

        public AnimationSupport()
        {
            settings = new Dictionary<View, SettingDictionary>();
        }

        public void SaveSettingsFor(List<View> views, List<BindableProperty> propertys)
        {
            foreach (View v in views)
            {
                SettingDictionary s = new SettingDictionary();

                foreach (BindableProperty bp in propertys)
                {
                    s.Add(bp, v.GetValue(bp));
                }
                settings.Add(v, s);
            }

        }

        public void Restore()
        {
            foreach (View v in settings.Keys)
            {
                settings.TryGetValue(v, out SettingDictionary s);

                if (s == null) return;

                foreach (BindableProperty bp in s.Keys)
                {

                    s.TryGetValue(bp, out object o);

                    v.SetValue(bp, o);

                }
            }
        }
    }
}
