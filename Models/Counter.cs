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

        private static readonly Dictionary<string, string> LegacyColors = new()
        {
            ["#2563EB"] = "Niebieski",
            ["#15803D"] = "Zielony",
            ["#C2410C"] = "Pomarańczowy",
            ["#7E22CE"] = "Fioletowy",
            ["#BE185D"] = "Różowy"
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
                string normalizedColor = NormalizeColor(value);

                if (!AvailableColors.Values.Contains(normalizedColor))
                {
                    throw new ArgumentException("Wybierz kolor z dostępnej listy.");
                }

                if (_color == normalizedColor)
                {
                    return;
                }

                _color = normalizedColor;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            }
        }

        public static string NormalizeColor(string color)
        {
            string normalizedColor = color.Trim().ToUpperInvariant();

            if (LegacyColors.TryGetValue(normalizedColor, out string? name))
            {
                return AvailableColors[name];
            }

            return normalizedColor;
        }
    }
}
