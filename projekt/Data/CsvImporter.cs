using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SystemRezerwacjiSal.Models;
using SystemRezerwacjiSal.Utils;

namespace SystemRezerwacjiSal.Data
{
    public class CsvImporter
    {
        public IEnumerable<Zajecia> Import(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ValidationException($"Plik nie istnieje: {filePath}");

            var list = new List<Zajecia>();
            var lines = File.ReadAllLines(filePath);

            bool hasHeader = lines[0].StartsWith("Typ,");
            int startIndex = hasHeader ? 1 : 0;

            for (int i = startIndex; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');

                if (parts.Length < 7)
                    throw new ValidationException($"Niepoprawny format CSV w wierszu {i + 1}: {line}");

                Zajecia z = parts[0].Trim().ToLower() switch
                {
                    "wyklad" => new Wyklad(),
                    "laboratorium" => new Laboratorium
                    {
                        Grupa = (parts.Length > 8 && !string.IsNullOrWhiteSpace(parts[8])) ? int.Parse(parts[8]) : 0
                    },
                    "projekt" => new Projekt
                    {
                        Grupa1 = (parts.Length > 8 && !string.IsNullOrWhiteSpace(parts[8])) ? int.Parse(parts[8]) : 0,
                        Grupa2 = (parts.Length > 9 && !string.IsNullOrWhiteSpace(parts[9])) ? int.Parse(parts[9]) : 0
                    },
                    _ => throw new ValidationException($"Nieznany typ zajęć w wierszu {i + 1}: {parts[0]}")
                };

                z.Kierunek = parts[1].Trim();
                z.Przedmiot = parts[2].Trim();
                z.Prowadzacy = parts[3].Trim();
                z.Sala = parts[4].Trim();
                z.Dzien = ParseDay(parts[5].Trim());

                z.GodzinaRozpoczecia = TimeSpan.ParseExact(parts[6].Trim(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
                z.GodzinaZakonczenia = TimeSpan.ParseExact(parts[7].Trim(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture);

                list.Add(z);
            }

            return list;
        }

        private DayOfWeek ParseDay(string input)
        {
            return input.Trim().ToLower() switch
            {
                "poniedzialek" => DayOfWeek.Monday,
                "wtorek" => DayOfWeek.Tuesday,
                "sroda" => DayOfWeek.Wednesday,
                "czwartek" => DayOfWeek.Thursday,
                "piatek" => DayOfWeek.Friday,
                "sobota" => DayOfWeek.Saturday,
                "niedziela" => DayOfWeek.Sunday,
                _ => throw new ValidationException($"Niepoprawny dzień tygodnia w CSV: {input}")
            };
        }

    }
}
