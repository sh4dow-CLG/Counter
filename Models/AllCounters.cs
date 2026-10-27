using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;
namespace Counter.Models
{
    internal class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; } = new(); //kolekcja obiektów klasy

        private readonly string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "counters.xml");


        public void SaveCounters()
        {
            XElement root = new XElement("Counters");
            foreach (var counter in Counters)
            {
                XElement counterElement = new XElement("Counter");

                XElement name = new XElement("Name", counter.Name);
                XElement value = new XElement("Value", counter.Value);
                counterElement.Add(name, value);

                root.Add(counterElement);
            }

            XDocument document = new XDocument(root);

            document.Save(filePath);
        }

        public void LoadCounters()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            XDocument document = XDocument.Load(filePath); //zczytuje XML do pamięci
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

                if (string.IsNullOrWhiteSpace(name) || !int.TryParse(valueText, out int value))
                {
                    throw new FormatException("Plik zawiera niepoprawną nazwę lub wartość licznika");
                }

                loadedCounters.Add(new Counter
                {
                    Name = name,
                    Value = value
                });
            }
            Counters.Clear();

            foreach (Counter counter in loadedCounters)
            {
                Counters.Add(counter);
            }
        }


    }

}
