using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Counter.Models
{
    public class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; } = new();
        public bool CanSave { get; private set; }

        private readonly string filePath = Path.Combine(
            FileSystem.Current.AppDataDirectory, "counters.xml");

        public void SaveCounters()
        {
            if (!CanSave)
            {
                throw new InvalidOperationException(
                    "Nie można zapisać zmian po nieudanym odczycie danych. Popraw plik i uruchom aplikację ponownie.");
            }

            XElement root = new XElement("Counters");

            foreach (Counter counter in Counters)
            {
                root.Add(new XElement("Counter",
                    new XElement("Name", counter.Name),
                    new XElement("Value", counter.Value),
                    new XElement("InitialValue", counter.InitialValue),
                    new XElement("Color", counter.Color)));
            }

            XDocument document = new XDocument(root);
            string temporaryPath = filePath + ".tmp";
            document.Save(temporaryPath);
            File.Move(temporaryPath, filePath, true);
        }

        public void LoadCounters()
        {
            CanSave = false;

            if (!File.Exists(filePath))
            {
                CanSave = true;
                return;
            }

            XDocument document = XDocument.Load(filePath);
            XElement? root = document.Root;

            if (root is null || root.Name != "Counters")
            {
                throw new FormatException("Plik nie zawiera głównego elementu Counters.");
            }

            List<Counter> loadedCounters = new();

            foreach (XElement counterElement in root.Elements("Counter"))
            {
                string? name = counterElement.Element("Name")?.Value;
                string? valueText = counterElement.Element("Value")?.Value;
                string initialValueText = counterElement.Element("InitialValue")?.Value ?? "0";
                string color = (counterElement.Element("Color")?.Value ?? Counter.DefaultColor)
                    .Trim().ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(name) ||
                    !int.TryParse(valueText, out int value) ||
                    !int.TryParse(initialValueText, out int initialValue) ||
                    !Counter.AvailableColors.Values.Contains(color))
                {
                    throw new FormatException("Plik zawiera niepoprawną nazwę, wartość lub kolor licznika.");
                }

                loadedCounters.Add(new Counter
                {
                    Name = name,
                    Value = value,
                    InitialValue = initialValue,
                    Color = color
                });
            }

            Counters.Clear();

            foreach (Counter counter in loadedCounters)
            {
                Counters.Add(counter);
            }

            CanSave = true;
        }

        public void DeleteCounter(Counter counter)
        {
            int index = Counters.IndexOf(counter);

            if (index < 0)
            {
                return;
            }

            Counters.RemoveAt(index);

            try
            {
                SaveCounters();
            }
            catch
            {
                Counters.Insert(index, counter);
                throw;
            }
        }
    }
}
