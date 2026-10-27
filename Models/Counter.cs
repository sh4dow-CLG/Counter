using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Counter.Models
{
    internal class Counter :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; } = string.Empty;

        private int _value = 0; 
        
        public int Value
        {
            get {
                return _value; 
            }
            set { 
                if (_value == value)
                    {
                        return;
                    }
                _value = value;

                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }
}
