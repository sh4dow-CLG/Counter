using System.ComponentModel;

namespace Counter.Models
{
    public class Counter : INotifyPropertyChanged
    {
        public const string DefaultColor = "#577C8A";

        public static Dictionary<string, string> AvailableColors { get; } = new()
        {
            ["Niebieski"] = DefaultColor,
            ["Zielony"] = "#4F726C",
            ["Pomarańczowy"] = "#B77B57",
            ["Fioletowy"] = "#70649A",
            ["Różowy"] = "#B481BB"
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; } = string.Empty;
        public int InitialValue { get; init; }

        private int _value;
        private string _color = DefaultColor;

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                if (!AvailableColors.Values.Contains(value))
                {
                    throw new ArgumentException("Wybierz kolor z dostępnej listy.");
                }

                if (_color == value)
                {
                    return;
                }

                _color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            }
        }

    }
}
