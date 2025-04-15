using com.democratia.Views;

namespace com.democratia;

public partial class MainPage : ContentPage
{
    

    public MainPage()
    {
        InitializeComponent();
        SetTheme();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            ImageButton button = (ImageButton)sender;
            button.Source = "light.png";
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            ImageButton button = (ImageButton)sender;
            button.Source = "dark.png";
        }
    }

    private void SetTheme()
    {
        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            switchImageButton.Source = "dark.png";
        }
        else
        {
            switchImageButton.Source = "light.png";
        }
    }
    private async void OnNavigateTapped(object sender, EventArgs e)
    {
        if (sender is Label)
        {
            await Navigation.PushAsync(new Creation());
        }
        else if (sender is Button) { 
            await Navigation.PushAsync(new Home());
        }
    }

}
