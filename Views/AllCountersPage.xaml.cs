namespace Counter.Views;

public partial class AllCountersPage : ContentPage
{
    private readonly Models.AllCounters allCounters = new();
    public AllCountersPage()
    {
        InitializeComponent();
        BindingContext = allCounters;
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

    private async Task countersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.Count == 0)
        {
            return;
        }

        Models.Counter counter = (Models.Counter)e.CurrentSelection[0];
        countersCollection.SelectedItem = null;

        await Navigation.PushAsync(new CounterPage(counter));
    }
}
