using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace com.democratia.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [RelayCommand]
        public static async Task NavigateTapped(string commande)
        {
            Debug.WriteLine("hello");
            try
            {
                if (Shell.Current.CurrentItem?.Route != commande) await Shell.Current.GoToAsync(commande);
                
            }
            catch (Exception ex)
            {
                Application.Current?.Windows[0]?.Page?.DisplayAlert("Error",$"Erreur lors de la navigation : {ex.Message}","OK");
            }
        }

    }
}
