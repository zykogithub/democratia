namespace com.democratia.Views.Component;

public partial class BackButton : ContentView
{
	public BackButton()
	{
		InitializeComponent();
	}

    private async void backButton_Clicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count>0)
        {
            await Navigation.PopAsync();
        }
    }
}