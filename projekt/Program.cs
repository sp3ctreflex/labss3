using System;
using System.Collections.Generic;
using SystemRezerwacjiSal.Core;
using SystemRezerwacjiSal.Data;
using SystemRezerwacjiSal.Models;
using SystemRezerwacjiSal.Utils;

namespace SystemRezerwacjiSal
{
    internal class Program
    {
        private PlanZajec plan;
        private IDataProvider db;
        private CsvImporter importer;
        private CsvExporter exporter;

        static void Main()
        {
            var p = new Program();
            p.Run();
        }

        void Run()
        {
            db = new SqlDataProvider();
            importer = new CsvImporter();
            exporter = new CsvExporter();
            plan = new PlanZajec();

            foreach (var z in db.Load())
                plan.Dodaj(z);

            while (true)
            {
                Console.WriteLine("\n=== System Rezerwacji Sal ===");
                Console.WriteLine("1. Dodaj zajęcia");
                Console.WriteLine("2. Usuń zajęcia");
                Console.WriteLine("3. Wypisz zajęcia dla dnia");
                Console.WriteLine("4. Wypisz zajęcia dla grupy");
                Console.WriteLine("5. Wypisz zajęcia dla sali");
                Console.WriteLine("6. Import CSV");
                Console.WriteLine("7. Export CSV");
                Console.WriteLine("8. Wypisz cały plan");
                Console.WriteLine("9. Sprawdz czy zaj dla grupy");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");

                string opcja = Console.ReadLine()?.Trim();

                try
                {
                    switch (opcja)
                    {
                        case "1": Dodaj(); break;
                        case "2": Usun(); break;
                        case "3": Wypisz(plan.DlaDnia(ReadDay())); break;
                        case "4": Wypisz(plan.DlaGrupy(ReadInt("Grupa"))); break;
                        case "5": Wypisz(plan.DlaSali(Read("Sala"))); break;
                        case "6": Import(); break;
                        case "7": Export(); break;
                        case "8": Wypisz(plan.Wszystkie); break;
                        case "9": SprawdzCzyDlaGrupy(); break;

                        case "0": return;
                        default: Console.WriteLine("Niepoprawna opcja."); break;
                    }
                }
                catch (Utils.ValidationException ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
                }
            }
        }

        void Dodaj()
        {
            Console.WriteLine("Wybierz typ zajęć: 1.Wyklad  2.Laboratorium  3.Projekt");
            string t = Console.ReadLine()?.Trim();

            Zajecia z = t switch
            {
                "1" => new Wyklad(),
                "2" => new Laboratorium { Grupa = ReadInt("Grupa") },
                "3" => new Projekt { Grupa1 = ReadInt("Grupa 1"), Grupa2 = ReadInt("Grupa 2") },
                _ => throw new Utils.ValidationException("Niepoprawny typ zajęć")
            };

            z.Kierunek = Read("Kierunek");
            z.Przedmiot = Read("Przedmiot");
            z.Prowadzacy = Read("Prowadzący");
            z.Sala = Read("Sala");
            z.Dzien = ReadDay();
            z.GodzinaRozpoczecia = ReadTime("Godzina rozp zajęć");
            z.GodzinaZakonczenia = ReadTime("Godzina zak zajęć");

            plan.Dodaj(z);
            db.Insert(z);

            Console.WriteLine("Zajęcia dodane poprawnie!");
        }

        void Usun()
        {
            Wypisz(plan.Wszystkie);
            int idx = ReadInt("Index zajęć do usunięcia") -1;

            if (idx < 0 || idx >= plan.Wszystkie.Count)
            {
                Console.WriteLine("Nieprawidłowy indeks.");
                return;
            }

            int idDoUsuniecia = plan.Wszystkie[idx].Id;
            plan.Usun(idx);
            db.Delete(idDoUsuniecia);

            Console.WriteLine("Zajęcia usunięte.");
        }

        void Import()
        {
            string file = Read("Nazwa lub ścieżka do pliku CSV do importu. Np.: abcd.csv");
            foreach (var z in importer.Import(file))
            {
                plan.Dodaj(z);
                db.Insert(z);
            }
            Console.WriteLine("Import zakończony.");
        }

        void Export()
        {
            string file = Read("Nazwa lub ścieżka do pliku CSV do eksportu. Np.: abcd.csv");
            exporter.Export(file, plan.Wszystkie);
            Console.WriteLine("Eksport zakończony.");
        }

        void Wypisz(IEnumerable<Zajecia> list)
        {
            string naglowek = string.Format(
                "{0,-4} | {1,-12} | {2,-13} | {3,-11} | {4,-6} | {5,-25} | {6,-20} | {7,-6} | {8,-15}",
                "Id", "Typ", "Dzien", "Godziny", "Kier", "Przedmiot", "Prowadzacy", "Sala", "Grupy"
            );
            Console.WriteLine(naglowek);
            Console.WriteLine(new string('-', naglowek.Length));

            int i = 0;
            foreach (var z in list)
            {
                string typ = z switch
                {
                    Wyklad => "Wyklad",
                    Laboratorium => "Laboratorium",
                    Projekt => "Projekt",
                    _ => "Nieznany"
                };

                string grupy = z switch
                {
                    Laboratorium lab => $"Grupa {lab.Grupa}",
                    Projekt proj => proj.Grupa2 > 0 ? $"Grupy {proj.Grupa1} i {proj.Grupa2}" : $"Grupa {proj.Grupa1}",
                    _ => ""
                };

                string godziny = $"{z.GodzinaRozpoczecia:hh\\:mm}-{z.GodzinaZakonczenia:hh\\:mm}".PadLeft(6).PadRight(11);

                Console.WriteLine(string.Format(
                    "{0,-4} | {1,-12} | {2,-13} | {3,-11} | {4,-6} | {5,-25} | {6,-20} | {7,-6} | {8,-15}",
                    z.Id, typ, DzienPolski(z.Dzien), godziny, z.Kierunek, z.Przedmiot, z.Prowadzacy, z.Sala, grupy
                ));
            }
        }
        void SprawdzCzyDlaGrupy()
        {
            Wypisz(plan.Wszystkie);

            int idZajec = ReadInt("Podaj ID zajęć do sprawdzenia");

            var zajecia = plan.Wszystkie.FirstOrDefault(z => z.Id == idZajec);
            if (zajecia == null)
            {
                Console.WriteLine("Nie znaleziono zajęć o podanym ID.");
                return;
            }

            int nrGrupy = ReadInt("Podaj numer grupy do sprawdzenia");

            bool wynik = zajecia.CzyDlaGrupy(nrGrupy);

            Console.WriteLine($"Zajęcia o ID {zajecia.Id} {(wynik ? "są" : "nie są")} dla grupy {nrGrupy}.");
        }

        static string Read(string name)
        {
            Console.Write($"{name}: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        static int ReadInt(string name)
        {
            string input = Read(name);
            if (!int.TryParse(input, out int val))
                throw new Utils.ValidationException($"Niepoprawna liczba: {input}");
            return val;
        }

        static DayOfWeek ReadDay()
        {
            string input = Read("Dzień tygodnia (Poniedzialek..Niedziela)").Trim().ToLower();
            return input switch
            {
                "poniedzialek" => DayOfWeek.Monday,
                "wtorek" => DayOfWeek.Tuesday,
                "sroda" => DayOfWeek.Wednesday,
                "czwartek" => DayOfWeek.Thursday,
                "piatek" => DayOfWeek.Friday,
                "sobota" => DayOfWeek.Saturday,
                "niedziela" => DayOfWeek.Sunday,
                _ => throw new Utils.ValidationException("Niepoprawny dzień tygodnia")
            };
        }

        static string DzienPolski(DayOfWeek dzien)
        {
            return dzien switch
            {
                DayOfWeek.Monday => "Poniedzialek",
                DayOfWeek.Tuesday => "Wtorek",
                DayOfWeek.Wednesday => "Sroda",
                DayOfWeek.Thursday => "Czwartek",
                DayOfWeek.Friday => "Piatek",
                DayOfWeek.Saturday => "Sobota",
                DayOfWeek.Sunday => "Niedziela",
                _ => dzien.ToString()
            };
        }
        static TimeSpan ReadTime(string name)
        {
            Console.Write($"{name} (HH:mm): ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (!TimeSpan.TryParse(input, out TimeSpan godz))
                throw new Utils.ValidationException("Niepoprawny format godziny, użyj HH:mm");
            return godz;
        }
    }
}
