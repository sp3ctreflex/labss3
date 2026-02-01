using System;
using System.Collections.Generic;

enum StatusZamowienia
{
    Oczekujace = 1,
    Przyjete,
    Zrealizowane,
    Anulowane
}

class Program
{
    static void Main()
    {
        Dictionary<int, List<string>> zamowienia = new Dictionary<int, List<string>>();
        Dictionary<int, StatusZamowienia> statusyZamowien = new Dictionary<int, StatusZamowienia>();

        int numerZamowienia = 1;
        bool kontynuuj = true;

        while (kontynuuj)
        {
            Console.WriteLine("=== System zarządzania zamówieniami ===");
            Console.WriteLine("1 - Dodaj zamówienie");
            Console.WriteLine("2 - Zmień status zamówienia");
            Console.WriteLine("3 - Wyświetl wszystkie zamówienia");
            Console.WriteLine("4 - Wyjście");
            Console.Write("Wybierz opcję: ");
            
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    DodajZamowienie(zamowienia, statusyZamowien, ref numerZamowienia);
                    break;
                case "2":
                    ZmienStatusZamowienia(statusyZamowien);
                    break;
                case "3":
                    WyswietlZamowienia(zamowienia, statusyZamowien);
                    break;
                case "4":
                    kontynuuj = false;
                    break;
                default:
                    Console.WriteLine("Niepoprawna opcja. Spróbuj ponownie.");
                    break;
            }

            Console.WriteLine();
        }

        Console.WriteLine("Koniec programu.");
    }

    static void DodajZamowienie(Dictionary<int, List<string>> zamowienia, Dictionary<int, StatusZamowienia> statusy, ref int numerZamowienia)
    {
        List<string> produkty = new List<string>();
        Console.WriteLine("Dodawanie produktów do zamówienia (wpisz 'koniec' aby zakończyć):");
        while (true)
        {
            Console.Write("Produkt: ");
            string produkt = Console.ReadLine();
            if (produkt.ToLower() == "koniec") break;
            produkty.Add(produkt);
        }

        zamowienia[numerZamowienia] = produkty;
        statusy[numerZamowienia] = StatusZamowienia.Oczekujace;
        Console.WriteLine($"Zamówienie nr {numerZamowienia} dodane z statusem Oczekujace.");
        numerZamowienia++;
    }

    static void ZmienStatusZamowienia(Dictionary<int, StatusZamowienia> statusy)
    {
        try
        {
            Console.Write("Podaj numer zamówienia: ");
            int nr = int.Parse(Console.ReadLine());

            if (!statusy.ContainsKey(nr))
                throw new KeyNotFoundException($"Zamówienie nr {nr} nie istnieje.");

            Console.WriteLine("Dostępne statusy:");
            foreach (var s in Enum.GetValues(typeof(StatusZamowienia)))
            {
                Console.WriteLine($"{(int)s} - {s}");
            }

            Console.Write("Wybierz nowy status: ");
            StatusZamowienia nowyStatus = (StatusZamowienia)int.Parse(Console.ReadLine());

            if (statusy[nr] == nowyStatus)
                throw new ArgumentException("Nowy status jest taki sam jak obecny.");

            statusy[nr] = nowyStatus;
            Console.WriteLine($"Status zamówienia nr {nr} zmieniony na {nowyStatus}.");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Błąd: Niepoprawny format danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
        }
    }

    static void WyswietlZamowienia(Dictionary<int, List<string>> zamowienia, Dictionary<int, StatusZamowienia> statusy)
    {
        Console.WriteLine("=== Lista zamówień ===");
        foreach (var zamowienie in zamowienia)
        {
            int nr = zamowienie.Key;
            List<string> produkty = zamowienie.Value;
            StatusZamowienia status = statusy[nr];

            Console.WriteLine($"Zamówienie nr {nr} - Status: {status}");
            Console.WriteLine("Produkty:");
            foreach (var p in produkty)
            {
                Console.WriteLine($" - {p}");
            }
        }
    }
}
