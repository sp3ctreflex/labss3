using System.ComponentModel.DataAnnotations;
using SystemRezerwacjiSal.Models;
using SystemRezerwacjiSal.Utils;

namespace SystemRezerwacjiSal.Core;

class PlanZajec
{
    private readonly List<Zajecia> _zajecia = new();

    public IReadOnlyList<Zajecia> Wszystkie => _zajecia;

    public void Dodaj(Zajecia z)
    {
        Waliduj(z);

        bool kolizja = _zajecia.Any(x =>
            x.Dzien == z.Dzien &&
            x.Sala == z.Sala &&
            x.GodzinaRozpoczecia < z.GodzinaZakonczenia &&
            z.GodzinaRozpoczecia < x.GodzinaZakonczenia &&
            KolizjaGrup(x, z)
        );

        if (kolizja)
            throw new Utils.ValidationException("Kolizja: sala lub grupa jest już zajęta.");

        _zajecia.Add(z);
    }

    public void Usun(int index)
    {
        if (index < 0 || index >= _zajecia.Count)
            throw new Utils.ValidationException("Nieprawidłowy indeks.");

        _zajecia.RemoveAt(index);
    }

    private bool KolizjaGrup(Zajecia a, Zajecia b)
    {
        for (int g = 1; g <= 10; g++)
        {
            if (a.CzyDlaGrupy(g) && b.CzyDlaGrupy(g))
                return true;
        }
        return false;
    }

    private void Waliduj(Zajecia z)
    {
        if (z.GodzinaRozpoczecia < TimeSpan.FromHours(6) || z.GodzinaZakonczenia > TimeSpan.FromHours(22))
            throw new Utils.ValidationException("Godzina musi być z zakresu 6:00–22:00.");

        if (z.GodzinaZakonczenia <= z.GodzinaRozpoczecia)
            throw new Utils.ValidationException("Godzina zakończenia musi być później niż godzina rozpoczęcia.");

        if (string.IsNullOrWhiteSpace(z.Sala))
            throw new Utils.ValidationException("Sala nie może być pusta.");
    }


    public IEnumerable<Zajecia> DlaDnia(DayOfWeek d)
        => _zajecia.Where(z => z.Dzien == d);

    public IEnumerable<Zajecia> DlaGrupy(int g)
        => _zajecia.Where(z => z.CzyDlaGrupy(g));

    public IEnumerable<Zajecia> DlaSali(string s)
        => _zajecia.Where(z => z.Sala == s);
}
