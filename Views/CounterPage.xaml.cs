namespace Counter.Views;

public partial class CounterPage : ContentPage
{
    private readonly Models.Counter counter;

    public CounterPage(Models.Counter counter)
    {

        InitializeComponent();


        this.counter = counter;
        BindingContext = counter;
    }

    private void AdditionButton_Clicked(object sender, EventArgs e)
    {
        counter.Value++;
    }

    private void SubtractButton_Clicked(object sender, EventArgs e)
    {
        counter.Value--;
    }
}
