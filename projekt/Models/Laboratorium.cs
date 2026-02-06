namespace SystemRezerwacjiSal.Models;

public class Laboratorium : Zajecia
{
    public int Grupa { get; set; }

    //public override bool CzyDlaGrupy(int nrGrupy) => Grupa == nrGrupy;
    public override bool CzyDlaGrupy(int nrGrupy)
    {
        return GrupyId.Contains(nrGrupy);
    }
    public override string Typ => $"Labor G{Grupa}";
}
