using System;
using System.Collections.Generic;
using System.IO;
using SystemRezerwacjiSal.Models;

namespace SystemRezerwacjiSal.Data
{
    public class CsvExporter
    {
        public void Export(string filePath, IEnumerable<Zajecia> zajecia)
        {
            using var writer = new StreamWriter(filePath);

            writer.WriteLine("Typ,Kierunek,Przedmiot,Prowadzacy,Sala,Dzien,GodzinaRozpoczecia,GodzinaZakonczenia,Grupa1,Grupa2");

            foreach (var z in zajecia)
            {
                string typ = z switch
                {
                    Wyklad => "Wyklad",
                    Laboratorium => "Laboratorium",
                    Projekt => "Projekt",
                    _ => "Nieznany"
                };

                string dzien = DzienPolski(z.Dzien);

                string grupa1 = "";
                string grupa2 = "";

                if (z is Laboratorium lab)
                    grupa1 = lab.Grupa.ToString();
                else if (z is Projekt proj)
                {
                    grupa1 = proj.Grupa1.ToString();
                    grupa2 = proj.Grupa2.ToString();
                }
                string line = $"{typ},{z.Kierunek},{z.Przedmiot},{z.Prowadzacy},{z.Sala},{dzien},{z.GodzinaRozpoczecia},{z.GodzinaZakonczenia},{grupa1},{grupa2}";
                writer.WriteLine(line);
            }
        }

        private string DzienPolski(DayOfWeek dzien)
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
    }
}
