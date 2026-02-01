using System;
using System.Collections.Generic;

enum Operacja
{
    Dodawanie = 1,
    Odejmowanie,
    Mnozenie,
    Dzielenie
}

class Program
{
    static void Main()
    {
        List<double> historiaWynikow = new List<double>();
        bool kontynuuj = true;

        while (kontynuuj)
        {
            try
            {
                Console.WriteLine("Kalkulator operacji matematycznych");
                Console.Write("Podaj pierwszą liczbę: ");
                double a = double.Parse(Console.ReadLine());

                Console.Write("Podaj drugą liczbę: ");
                double b = double.Parse(Console.ReadLine());

                Console.WriteLine("Wybierz operację:");
                Console.WriteLine("1 - Dodawanie, 2 - Odejmowanie, 3 - Mnożenie, 4 - Dzielenie");
                Operacja operacja = (Operacja)int.Parse(Console.ReadLine());

                double wynik = WykonajOperacje(a, b, operacja);
                Console.WriteLine($"Wynik: {wynik}");

                historiaWynikow.Add(wynik);

                Console.WriteLine("Poprzednie wyniki:");
                foreach (var w in historiaWynikow)
                {
                    Console.WriteLine(w);
                }
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Błąd: Próba dzielenia przez zero!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Błąd: Niepoprawny format liczby!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
            }

            Console.Write("Czy chcesz kontynuować? (t/n): ");
            string odp = Console.ReadLine().ToLower();
            if (odp != "t") kontynuuj = false;

            Console.WriteLine();
        }

        Console.WriteLine("Koniec programu. Dziękujemy za skorzystanie z kalkulatora!");
    }

    static double WykonajOperacje(double a, double b, Operacja operacja)
    {
        switch (operacja)
        {
            case Operacja.Dodawanie:
                return a + b;
            case Operacja.Odejmowanie:
                return a - b;
            case Operacja.Mnozenie:
                return a * b;
            case Operacja.Dzielenie:
                if (b == 0)
                    throw new DivideByZeroException();
                return a / b;
            default:
                throw new ArgumentOutOfRangeException("Niepoprawna operacja!");
        }
    }
}
