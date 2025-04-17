namespace com.democratia.Views.Component;
using com.democratia.Resources.Styles;


public partial class EntryComponent : ContentView
{
    private string _title;
    private string _valeurDonne;
    private bool _passWord;

    // Propriété publique pour passer un paramètre  
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            // Met à jour le texte du Label si nécessaire  
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
            // Met à jour le paramètre si nécessaire  
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
            // Met à jour le texte du Label si nécessaire  
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

        // Ajout de l'abonnement à l'événement TextChanged  
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
