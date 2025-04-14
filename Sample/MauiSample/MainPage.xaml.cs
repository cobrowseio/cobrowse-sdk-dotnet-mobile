using Cobrowse.IO;

namespace MauiSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Title = "Home";
    }

    private void OnCobrowseClicked(object sender, EventArgs e)
    {
        this.Navigation.PushAsync(new CobrowseCodePage());
    }
}


