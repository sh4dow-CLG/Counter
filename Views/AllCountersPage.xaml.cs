
namespace Counter.Views;

public partial class AllCountersPage : ContentPage
{
    private readonly Models.AllCounters allCounters = new();
    private bool dataLoaded = false;
    public AllCountersPage()
    {
        InitializeComponent();
        BindingContext = allCounters;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (dataLoaded)
        {
            return;
        }
        dataLoaded = true;

        try
        {
            allCounters.LoadCounters();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Błąd odczytu", ex.Message, "OK");
        }
    }

    private async void Add_counter_clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlertAsync(
                "Brak nazwy",
                "Wpisz nazwę licznika.",
                "OK");
            return;
        }
        allCounters.Counters.Add(new Models.Counter
        {
            Name = name

        });


        NameEntry.Text = string.Empty;
    }

    private async void countersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
        {
            return;
        }

        Models.Counter counter = (Models.Counter)e.CurrentSelection[0]; //określa typ
        countersCollection.SelectedItem = null; //usuwa zaznaczenie aby po prowocie wybrac ten sam licznik

        await Navigation.PushAsync(new CounterPage(counter));
    }

    private async void Save_clicked(object sender, EventArgs e)
    {
        try
        {
            allCounters.SaveCounters();

            await DisplayAlertAsync(
                "Zapis",
                "Liczniki zostały zapisane.",
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Błąd zapisu", ex.Message, "OK");
        }
    }
}
