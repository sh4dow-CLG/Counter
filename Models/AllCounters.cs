using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Counter.Models
{
    internal class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; } = new(); //kolekcja obiektów klasy
    }
}
