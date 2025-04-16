namespace com.democratia.Views.Component;

public partial class SwitchButton : ContentView
{
	public SwitchButton()
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
}