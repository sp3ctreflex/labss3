namespace SystemRezerwacjiSal.Models;

public class Wyklad : Zajecia
{
    //public override bool CzyDlaGrupy(int nrGrupy) => true;
    public override bool CzyDlaGrupy(int nrGrupy)
    {
        return GrupyId.Count == 0 || GrupyId.Contains(nrGrupy);
    }
    public override string Typ => "Wyklad";
}
