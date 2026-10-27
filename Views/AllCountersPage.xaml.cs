namespace Counter.Views;

public partial class AllCountersPage : ContentPage
{
    private readonly Models.AllCounters allCounters = new();
    private bool loadAttempted;

    public AllCountersPage()
    {
        InitializeComponent();
        BindingContext = allCounters;
        ColorPicker.ItemsSource = Models.Counter.AvailableColors.Keys.ToList();
        ColorPicker.SelectedIndex = 0;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (loadAttempted)
        {
            return;
        }

        loadAttempted = true;

        try
        {
            allCounters.LoadCounters();
        }
        catch (Exception ex) when (
            ex is IOException || ex is UnauthorizedAccessException ||
            ex is System.Xml.XmlException || ex is FormatException)
        {
            CreationForm.IsEnabled = false;
            SaveButton.IsEnabled = false;
            await DisplayAlertAsync("Błąd odczytu", ex.Message, "OK");
        }
    }

    private async void Add_counter_clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlertAsync("Brak nazwy", "Wpisz nazwę licznika.", "OK");
            return;
        }

        if (!int.TryParse(InitialValueEntry.Text, out int initialValue))
        {
            await DisplayAlertAsync("Niepoprawna wartość", "Wpisz liczbę całkowitą z zakresu int.", "OK");
            return;
        }

        string colorName = ColorPicker.SelectedItem as string ?? "Niebieski";
        Models.Counter counter = new()
        {
            Name = name,
            InitialValue = initialValue,
            Value = initialValue,
            Color = Models.Counter.AvailableColors[colorName]
        };

        allCounters.Counters.Add(counter);

        try
        {
            allCounters.SaveCounters();
            NameEntry.Text = string.Empty;
            InitialValueEntry.Text = "0";
        }
        catch (Exception ex) when (
            ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
        {
            allCounters.Counters.Remove(counter);
            await DisplayAlertAsync("Błąd autozapisu", ex.Message, "OK");
        }
    }

    private async void countersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
        {
            return;
        }

        Models.Counter counter = (Models.Counter)e.CurrentSelection[0];
        countersCollection.SelectedItem = null;
        await Navigation.PushAsync(new CounterPage(counter, allCounters));
    }

    private async void Save_clicked(object sender, EventArgs e)
    {
        try
        {
            allCounters.SaveCounters();
            await DisplayAlertAsync("Zapis", "Liczniki zostały zapisane.", "OK");
        }
        catch (Exception ex) when (
            ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
        {
            await DisplayAlertAsync("Błąd zapisu", ex.Message, "OK");
        }
    }
}
