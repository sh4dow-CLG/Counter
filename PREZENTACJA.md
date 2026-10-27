# Prezentacja projektu Counter — około 4 minut

## 0:00–0:30 — Cel

„Jestem Franciszek Wawrzeń. Counter to aplikacja .NET MAUI do obsługi wielu nazwanych liczników. Każdy ma wartość aktualną, początkową i kolor. Dane są przechowywane lokalnie w XML.”

## 0:30–1:15 — Tworzenie

Dodaj Pompki od 5 w kolorze niebieskim i Książki od 0 w kolorze zielonym. Pokaż oba na liście. Otwórz Pompki i pokaż, że wartość początkowa jest tylko wyświetlana.

## 1:15–2:00 — Plus, minus i autozapis

Zwiększ Pompki do 7 i użyj minusa. Zamknij aplikację bez naciskania Zapisz, uruchom ponownie i pokaż odtworzoną wartość oraz kolor.

„Przycisk zmienia właściwość Value i wywołuje zapis całej kolekcji. Ręczny przycisk zapisu jest dodatkową możliwością.”

## 2:00–2:45 — Reset, kolor, usuwanie

Zresetuj Pompki do 5. Zmień kolor na fioletowy. Usuń Książki, potwierdź i uruchom aplikację ponownie, aby pokazać trwałość zmian.

## 2:45–4:00 — Najważniejsze pliki

- Counter.cs opisuje dane i wysyła PropertyChanged przy zmianie Value oraz Color.
- AllCounters.cs ma ObservableCollection i metody SaveCounters, LoadCounters, DeleteCounter.
- XAML opisuje ekran, a .xaml.cs obsługuje zdarzenia użytkownika.
- XDocument zapisuje XML w FileSystem.Current.AppDataDirectory.
- InitialValue ma init, więc ustawiamy ją przy tworzeniu obiektu. Reset zmienia Value na InitialValue.

# Pytania i krótkie odpowiedzi

**Czym jest model?** Klasa opisująca dane jednego licznika. Wiele obiektów tej samej klasy ma osobne wartości.

**Co daje ObservableCollection?** Zgłasza zmianę zawartości kolekcji, np. dodanie i usunięcie. CollectionView aktualizuje wtedy listę.

**Co to data binding?** Powiązanie właściwości kontrolki z właściwością obiektu wskazanego przez BindingContext.

**Po co INotifyPropertyChanged?** Informuje binding, że zmieniła się właściwość konkretnego obiektu, np. Value lub Color.

**Jak działa Clicked?** Kliknięcie przycisku wywołuje metodę wskazaną w XAML. Metoda zmienia dane i zapisuje stan.

**Jak zapisujemy XML?** Tworzymy XDocument z elementem Counters i osobnym Counter dla każdego obiektu. Każdy zapisuje Name, Value, InitialValue i Color.

**Jak odczytujemy XML?** Load wczytuje dokument, Elements wybiera liczniki, Element pobiera ich pola. Sprawdzamy dane i tworzymy obiekty Counter.

**Gdzie są dane?** W pliku counters.xml w FileSystem.Current.AppDataDirectory — systemowym katalogu danych aplikacji.

**Po co dwie wartości?** Value zmienia się podczas liczenia. InitialValue pamięta punkt startowy potrzebny do resetu.

**Kiedy działa autozapis?** Po dodaniu, plusie, minusie, resecie, zmianie koloru i usunięciu.

**Dlaczego init?** Pozwala ustawić właściwość podczas tworzenia obiektu i blokuje późniejsze zwykłe przypisanie.

**Co oznacza protected override OnAppearing?** Nadpisujemy odziedziczoną metodę cyklu życia strony. Jest protected jak w klasie bazowej. Flaga loadAttempted pozwala wczytać dane raz dla danej strony.

**Co to Task<bool>?** Reprezentuje operację, na której zakończenie można poczekać przez await, a następnie otrzymać true lub false. SaveChangesAsync zwraca, czy zapis się udał.

**Co przy błędzie zapisu?** Pokazujemy komunikat i cofamy zmianę wartości, koloru lub usunięcie. Przy błędzie odczytu zapis jest blokowany, aby nie zniszczyć istniejącego pliku.

**Dlaczego kod jest w code-behind?** To mała aplikacja edukacyjna. Oddzielne modele i obsługa zdarzeń na stronach są wystarczające, czytelne i zgodne ze wzorem projektu.
