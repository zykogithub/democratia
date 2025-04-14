using com.democratia.Resources.Styles;

namespace com.democratia
{
    public partial class App : Application
    {
        public App()
        {
            SetTheme();
            InitializeComponent();
            MainPage = new AppShell();
        }

        public static void SetTheme()
        {
            // Exemple de sélection de thème basé sur le thème du système
            Current.Resources.MergedDictionaries.Clear();
            

            if (Current.RequestedTheme == AppTheme.Dark) { 
                Current.Resources.MergedDictionaries.Add(new Dark());
                Current.Resources.MergedDictionaries.Add(new com.democratia.Resources.Styles.Colors());
                Current.Resources.MergedDictionaries.Add(new com.democratia.Resources.Styles.Styles());
            }

            else
            { 
                Current.Resources.MergedDictionaries.Add(new Light());
                Current.Resources.MergedDictionaries.Add(new com.democratia.Resources.Styles.Colors());
                Current.Resources.MergedDictionaries.Add(new com.democratia.Resources.Styles.Styles());
            }

        }
    }
}
