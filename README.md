# Counter

Autor: Franciszek Wawrzeń.

Prosta aplikacja .NET MAUI do tworzenia i obsługi wielu nazwanych liczników.

## Funkcje

- Dodawanie liczników z nazwą, wartością początkową i wybranym kolorem.
- Zwiększanie i zmniejszanie wartości.
- Wartość początkowa jest ustawiana wyłącznie przy tworzeniu licznika.
- Automatyczny zapis po dodaniu, zmianie wartości, resetowaniu, zmianie koloru i usunięciu.
- Odtwarzanie wszystkich danych po ponownym uruchomieniu.
- Reset do wartości początkowej, usuwanie z potwierdzeniem i powrót do listy.
- Obsługa pustej nazwy, błędnej liczby, zakresu int i problemów z plikiem.

## Technologie i uruchomienie

C#, XAML, .NET MAUI 10, CollectionView, ObservableCollection, INotifyPropertyChanged, LINQ to XML (XDocument).

Projekt jest obecnie skonfigurowany do uruchamiania na Windows. Potrzebne są SDK .NET 10 oraz narzędzia .NET MAUI dla Windows, np. w Visual Studio 2026.

1. Otwórz Counter.sln w Visual Studio.
2. Wybierz Windows Machine / Komputer z systemem Windows.
3. Uruchom F5.

Kompilacja w terminalu otwartym w folderze projektu:

```powershell
dotnet build Counter.sln
```

## Zapis danych

Plik counters.xml znajduje się w FileSystem.Current.AppDataDirectory. Ścieżka nie jest przypisana na sztywno do komputera autora. Zapisywane są Name, Value, InitialValue oraz Color jako HEX.

Najpierw zapisywany jest plik tymczasowy, a potem zastępuje on właściwy plik danych. Odczyt sprawdza cały dokument przed zmianą kolekcji. Błąd odczytu blokuje zapis, aby nie nadpisać istniejących danych pustą listą. Starsze pliki bez InitialValue lub Color otrzymują domyślnie 0 i kolor niebieski.

## Struktura

- Models/Counter.cs — dane jednego licznika i powiadamianie o zmianach.
- Models/AllCounters.cs — kolekcja, zapis i odczyt XML oraz trwałe usuwanie.
- Views/AllCountersPage.xaml i .xaml.cs — formularz tworzenia i lista.
- Views/CounterPage.xaml i .xaml.cs — plus/minus, reset, kolor i usuwanie.
- AppShell.xaml — strona startowa i nawigacja.

## Wymagania i prezentacja

| Wymaganie | Gdzie zrealizowane | Jak zaprezentować |
|---|---|---|
| Wartość i przyciski plus/minus (2) | CounterPage, Counter.Value | Otwórz pojedynczy licznik i kliknij przyciski. Etap jednego licznika rozbudowano o dynamiczną listę. |
| Dynamiczne dodawanie (3) | AllCountersPage.Add_counter_clicked, Counters | Dodaj dwa liczniki. |
| Nazwy liczników (3) | Counter.Name, NameEntry, Binding Name | Nadaj różne nazwy i pokaż je na liście. |
| System zapisu (4) | AllCounters.SaveCounters, Save_clicked | Zapisz i ponownie uruchom aplikację. |
| Wczytywanie na starcie (4) | OnAppearing, AllCounters.LoadCounters | Po uruchomieniu pokaż odtworzoną listę. |
| Autozapis po zmianie wartości (5) | AdditionButton_Clicked, SubtractButton_Clicked, SaveChangesAsync | Zmień wartość, zamknij bez ręcznego zapisu i uruchom ponownie. |
| Wartość początkowa tylko przy tworzeniu (5) | InitialValueEntry, Counter.InitialValue z init | Utwórz licznik od 5; później wartość początkowa jest tylko wyświetlana. |
| Personalizacja koloru (6) | Picker, Counter.AvailableColors, Counter.Color | Wybierz różne kolory i zmień kolor na stronie szczegółów. |
| Odtwarzanie koloru (6) | Element Color w SaveCounters i LoadCounters | Zamknij aplikację i pokaż zachowany kolor. |
| Dane w XML (6) | XDocument, counters.xml | Omów elementy Counter, Name, Value, InitialValue, Color. |
| Reset do wartości początkowej (6) | Reset_clicked | Zwiększ licznik od 5 do 7 i zresetuj do 5. |
| Usuwanie liczników (6) | Delete_clicked, AllCounters.DeleteCounter | Usuń licznik i sprawdź po ponownym uruchomieniu. |

## Test końcowy w aplikacji

1. Dodaj kilka liczników z różnymi nazwami, wartościami początkowymi i kolorami.
2. Zwiększaj i zmniejszaj ich wartości.
3. Zamknij aplikację bez używania przycisku Zapisz i uruchom ponownie.
4. Sprawdź odtworzone nazwy, wartości aktualne, początkowe i kolory.
5. Zresetuj jeden licznik i zmień kolor drugiego.
6. Usuń jeden licznik.
7. Ponownie uruchom aplikację i sprawdź trwałość zmian.
8. Sprawdź pustą nazwę i tekst zamiast liczby. Obsługę uszkodzonego XML testuj wyłącznie na kopii danych.

Scenariusz prezentacji i pytania nauczyciela znajdują się w PREZENTACJA.md.
