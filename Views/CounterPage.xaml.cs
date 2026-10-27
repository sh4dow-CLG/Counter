namespace Counter.Views;

public partial class CounterPage : ContentPage
{
    private readonly Models.Counter counter;
    private readonly Models.AllCounters allCounters;

    public CounterPage(Models.Counter counter, Models.AllCounters allCounters)
    {
        InitializeComponent();
        this.counter = counter;
        this.allCounters = allCounters;
        BindingContext = counter;
        ColorPicker.ItemsSource = Models.Counter.AvailableColors.Keys.ToList();
        SelectCurrentColor();
    }

    private async void AdditionButton_Clicked(object sender, EventArgs e)
    {
        if (counter.Value == int.MaxValue)
        {
            await DisplayAlertAsync("Zakres", "Osiągnięto maksymalną wartość licznika.", "OK");
            return;
        }

        int previousValue = counter.Value;
        counter.Value++;

        if (!await SaveChangesAsync())
        {
            counter.Value = previousValue;
        }
    }

    private async void SubtractButton_Clicked(object sender, EventArgs e)
    {
        if (counter.Value == int.MinValue)
        {
            await DisplayAlertAsync("Zakres", "Osiągnięto minimalną wartość licznika.", "OK");
            return;
        }

        int previousValue = counter.Value;
        counter.Value--;

        if (!await SaveChangesAsync())
        {
            counter.Value = previousValue;
        }
    }

    private async void Reset_clicked(object sender, EventArgs e)
    {
        int previousValue = counter.Value;
        counter.Value = counter.InitialValue;

        if (!await SaveChangesAsync())
        {
            counter.Value = previousValue;
        }
    }

    private async void Color_changed(object sender, EventArgs e)
    {
        if (ColorPicker.SelectedItem is not string name ||
            !Models.Counter.AvailableColors.TryGetValue(name, out string? color) ||
            color == counter.Color)
        {
            return;
        }

        string previousColor = counter.Color;
        counter.Color = color;

        if (!await SaveChangesAsync())
        {
            counter.Color = previousColor;
            SelectCurrentColor();
        }
    }

    private void SelectCurrentColor()
    {
        ColorPicker.SelectedItem = Models.Counter.AvailableColors
            .First(option => option.Value == counter.Color).Key;
    }

    private async void Delete_clicked(object sender, EventArgs e)
    {
        bool confirmed = await DisplayAlertAsync(
            "Usuwanie", $"Usunąć licznik „{counter.Name}”?", "Usuń", "Anuluj");

        if (!confirmed)
        {
            return;
        }

        try
        {
            allCounters.DeleteCounter(counter);
            await Navigation.PopAsync();
        }
        catch (Exception ex) when (
            ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
        {
            await DisplayAlertAsync("Błąd usuwania", ex.Message, "OK");
        }
    }

    private async Task<bool> SaveChangesAsync()
    {
        try
        {
            allCounters.SaveCounters();
            return true;
        }
        catch (Exception ex) when (
            ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
        {
            await DisplayAlertAsync("Błąd autozapisu", ex.Message, "OK");
            return false;
        }
    }

    private async void Back_clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
