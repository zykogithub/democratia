namespace com.democratia.Views.Component;
using com.democratia.Resources.Styles;


public partial class EntryComponent : ContentView
{
    private string _title;
    private string _valeurDonne;
    private bool _passWord;

    // Propri�t� publique pour passer un param�tre  
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            // Met � jour le texte du Label si n�cessaire  
            if (Content is VerticalStackLayout layout && layout.Children[0] is Label label)
            {
                label.Text = _title;
            }
        }
    }

    public string valeurDonne
    {
        get => _valeurDonne;
        set
        {
            // Met � jour le param�tre si n�cessaire  
            if (Content is VerticalStackLayout layout && layout.Children[2] is Entry)
            {
                _valeurDonne = value;
            }
        }
    }

    public bool? passWord
    {
        get => _passWord;
        set
        {
            _passWord = (bool)value;
            // Met � jour le texte du Label si n�cessaire  
            if (Content is VerticalStackLayout layout && layout.Children[2] is Entry entry)
            {
                entry.IsPassword = _passWord;
            }
        }
    }

    public EntryComponent()
    {
        Content = new VerticalStackLayout
        {
            Children = {
               new Label {
                   HorizontalOptions = LayoutOptions.Center,
                   Text = _title,
                   Style = (Style)Application.Current.Resources["SubHeadlineStyle"]
               },
               new BoxView
               {
                   HeightRequest = 30
               },
               new Entry
               {
                   Style = (Style)Application.Current.Resources["EntryStyle"],
                   MaximumWidthRequest = 300,
               },
               new BoxView
               {
                   HeightRequest = 30
               }
           }
        };

        // Ajout de l'abonnement � l'�v�nement TextChanged  
        if (Content is VerticalStackLayout layout && layout.Children[2] is Entry entry)
        {
            entry.TextChanged += OnTitleChanged;
            entry.IsPassword = _passWord;
        }
    }

    private void OnTitleChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            _valeurDonne = e.NewTextValue;
        }
    }
}
