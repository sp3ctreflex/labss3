using System;
using System.Collections.Generic;

enum Kolor
{
    Czerwony = 1,
    Niebieski,
    Zielony,
    Zolty,
    Fioletowy
}

class Program
{
    static void Main()
    {
        List<Kolor> kolory = new List<Kolor>
        {
            Kolor.Czerwony,
            Kolor.Niebieski,
            Kolor.Zielony,
            Kolor.Zolty,
            Kolor.Fioletowy
        };

        Random rnd = new Random();
        Kolor wylosowanyKolor = kolory[rnd.Next(kolory.Count)];
        bool trafiony = false;

        Console.WriteLine("=== Gra w zgadywanie kolorów ===");
        Console.WriteLine("Dostępne kolory: Czerwony, Niebieski, Zielony, Zolty, Fioletowy");

        while (!trafiony)
        {
            try
            {
                Console.Write("Zgadnij kolor: ");
                string input = Console.ReadLine();

                if (!Enum.TryParse(input, true, out Kolor zgadnietyKolor))
                    throw new ArgumentException("Niepoprawny kolor. Spróbuj ponownie.");

                if (!kolory.Contains(zgadnietyKolor))
                    throw new ArgumentException("Kolor nie znajduje się na liście dostępnych kolorów.");

                if (zgadnietyKolor == wylosowanyKolor)
                {
                    Console.WriteLine("Gratulacje! Trafiłeś właściwy kolor!");
                    trafiony = true;
                }
                else
                {
                    Console.WriteLine("Nie trafione. Spróbuj ponownie!");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
            }
        }

        Console.WriteLine("Koniec gry. Dziękujemy za zabawę!");
    }
}
