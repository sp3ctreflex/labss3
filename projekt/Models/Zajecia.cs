namespace SystemRezerwacjiSal.Models;

public abstract class Zajecia
{
    public int Id { get; set; }
    public string Kierunek { get; set; }
    public string Przedmiot { get; set; }
    public string Prowadzacy { get; set; }
    public string Sala { get; set; }
    public DayOfWeek Dzien { get; set; }
    public TimeSpan GodzinaRozpoczecia { get; set; }
    public TimeSpan GodzinaZakonczenia { get; set; }

    public List<int> GrupyId { get; set; } = new List<int>();
    public virtual bool CzyDlaGrupy(int nrGrupy)
    {
        return GrupyId.Contains(nrGrupy);
    }

    public virtual string Typ => "Zajecia";

    public override string ToString()
    {
        string godziny = $"{GodzinaRozpoczecia:hh\\:mm}-{GodzinaZakonczenia:hh\\:mm}";

        string grupaInfo = string.Empty;

        if (this is Laboratorium lab)
        {
            grupaInfo = $" | Grupa {lab.Grupa}";
        }
        else if (this is Projekt proj)
        {
            grupaInfo = $" | Grupy {proj.Grupa1}" + (proj.Grupa2 > 0 ? $", {proj.Grupa2}" : "");
        }

        return $"{Typ,-12} | {Dzien,-10} | {godziny} | {Kierunek,-10} | {Przedmiot,-25} | {Prowadzacy,-20} | Sala {Sala}{grupaInfo}";
    }



}
