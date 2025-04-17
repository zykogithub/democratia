namespace com.democratia.Views.Component;



public partial class Header : ContentView
{
    public Header()
    {
        InitializeComponent();
        SetTheme();
    }


    private async void OnClicked(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        if (button == switchImageButton)
        {
            if (Application.Current.RequestedTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                button.Source = "light.png";
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                button.Source = "dark.png";
            }
        }
        else if (button == backButton)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                await Navigation.PopAsync();
            }
        }
        else
        {
            await Navigation.PopToRootAsync();
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
}
