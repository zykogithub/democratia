using com.democratia.Views.Pages;

namespace com.democratia;

public partial class MainPage : ContentPage
{
    

    public MainPage()
    {
        InitializeComponent();
        
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
