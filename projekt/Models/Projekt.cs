namespace SystemRezerwacjiSal.Models;

public class Projekt : Zajecia
{
    public int Grupa1 { get; set; }
    public int Grupa2 { get; set; }

    //public override bool CzyDlaGrupy(int nrGrupy)
    //    => nrGrupy == Grupa1 || nrGrupy == Grupa2;
    public override bool CzyDlaGrupy(int nrGrupy)
    {
        return GrupyId.Contains(nrGrupy);
    }

    public override string Typ => $"Projekt G{Grupa1},{Grupa2}";
}
