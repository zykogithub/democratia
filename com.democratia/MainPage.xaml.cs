using com.democratia.Views.Pages;
using System.ComponentModel;

namespace com.democratia;

public partial class MainPage : ContentPage
{
    

    public MainPage()
    {
        InitializeComponent();
        ProcessusArrierePlan();
        
    }
    public void backgroundWorker_doWork(object sender, DoWorkEventArgs e)
    {
        BackgroundWorker? backgroundWorker = sender as BackgroundWorker;
        for (long i = 0; i < 100_000_000_000; i++)
        {
            
            if (i%10_000_000==0)
            {
                backgroundWorker?.ReportProgress((int)i);
            }

        }

    }

    public void backgroundWorker_progressChanged(object sender, ProgressChangedEventArgs e)
    {
        seConnecterLabel.Text = "Le thread a progressé de " + e.ProgressPercentage.ToString() + "%";
    }

    public void backgroundWorker_workerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (!e.Cancelled && e.Error == null) seConnecterLabel.Text = "Le thread a réussi";
        else if(e.Cancelled) seConnecterLabel.Text = "Le thread a été annulé";
        else seConnecterLabel.Text = "Le thread a échoué avec une erreur : " + e.Error.Message;
    }

    private void ProcessusArrierePlan()
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        backgroundWorker.WorkerReportsProgress = true;
        backgroundWorker.WorkerSupportsCancellation = true;
        backgroundWorker.DoWork += backgroundWorker_doWork;
        backgroundWorker.ProgressChanged += backgroundWorker_progressChanged;
        backgroundWorker.RunWorkerCompleted += backgroundWorker_workerCompleted;
        if (!backgroundWorker.IsBusy) backgroundWorker.RunWorkerAsync();
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
