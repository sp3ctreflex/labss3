using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SystemRezerwacjiSal.Models;
using SystemRezerwacjiSal.Utils;

namespace SystemRezerwacjiSal.Data
{
    public class SqlDataProvider : IDataProvider
    {
        private readonly string connectionString = @"Server=.\SQLEXPRESS;Database=RezerwacjaSal;User Id=abcd;Password=12345678;";

        public List<Zajecia> Load()
        {
            var list = new List<Zajecia>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        z.ZajeciaId, 
                        z.PrzedmiotId, 
                        z.ProwadzacyId, 
                        z.SalaId, 
                        z.FormaZajecId, 
                        z.DzienTygodnia, 
                        z.GodzinaRozpoczecia,
                        z.GodzinaZakonczenia,
                        p.Nazwa AS Przedmiot, 
                        k.Nazwa AS Kierunek, 
                        pr.Imie + ' ' + pr.Nazwisko AS Prowadzacy, 
                        s.Nazwa AS Sala
                    FROM Zajecia z
                    JOIN Przedmiot p ON z.PrzedmiotId = p.PrzedmiotId
                    JOIN Kierunek k ON p.KierunekId = k.KierunekId
                    JOIN Prowadzacy pr ON z.ProwadzacyId = pr.ProwadzacyId
                    JOIN Sala s ON z.SalaId = s.SalaId
                    JOIN FormaZajec f ON z.FormaZajecId = f.FormaZajecId";

                using var cmd = new SqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Zajecia z = reader.GetInt32(reader.GetOrdinal("FormaZajecId")) switch
                    {
                        1 => new Laboratorium(),
                        2 => new Wyklad(),
                        3 => new Projekt(),
                        _ => throw new Utils.ValidationException("Nieznana forma zajęć w SQL")
                    };

                    z.Id = reader.GetInt32(reader.GetOrdinal("ZajeciaId"));
                    z.Kierunek = reader.GetString(reader.GetOrdinal("Kierunek"));
                    z.Przedmiot = reader.GetString(reader.GetOrdinal("Przedmiot"));
                    z.Prowadzacy = reader.GetString(reader.GetOrdinal("Prowadzacy"));
                    z.Sala = reader.GetString(reader.GetOrdinal("Sala"));

                    string dzienSql = reader.GetString(reader.GetOrdinal("DzienTygodnia"));
                    z.Dzien = dzienSql switch
                    {
                        "Poniedzialek" => DayOfWeek.Monday,
                        "Wtorek" => DayOfWeek.Tuesday,
                        "Sroda" => DayOfWeek.Wednesday,
                        "Czwartek" => DayOfWeek.Thursday,
                        "Piatek" => DayOfWeek.Friday,
                        "Sobota" => DayOfWeek.Saturday,
                        "Niedziela" => DayOfWeek.Sunday,
                        _ => throw new Utils.ValidationException($"Niepoprawny dzień tygodnia w bazie SQL: {dzienSql}")
                    };

                    z.GodzinaRozpoczecia = reader.GetTimeSpan(reader.GetOrdinal("GodzinaRozpoczecia"));
                    z.GodzinaZakonczenia = reader.GetTimeSpan(reader.GetOrdinal("GodzinaZakonczenia"));

                    list.Add(z);
                }

                foreach (var z in list)
                {
                    var grupy = GetGrupy(z.Id);


                    z.GrupyId = grupy;

                    if (z is Laboratorium lab)
                    {
                        if (grupy.Count > 0)
                            lab.Grupa = grupy[0];
                        else
                            Console.WriteLine($"Uwaga: brak grup dla Laboratorium Id={z.Id}");
                    }
                    else if (z is Projekt proj)
                    {
                        if (grupy.Count > 0) proj.Grupa1 = grupy[0];
                        if (grupy.Count > 1) proj.Grupa2 = grupy[1];

                        if (grupy.Count == 0)
                            Console.WriteLine($"Uwaga: brak grup dla Projektu Id={z.Id}");
                    }
                }

            }

            return list;
        }

        public void Insert(Zajecia z)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = @"
                INSERT INTO Zajecia (PrzedmiotId, ProwadzacyId, SalaId, FormaZajecId, DzienTygodnia, GodzinaRozpoczecia, GodzinaZakonczenia)
                VALUES (@przedmiotId, @prowadzacyId, @salaId, @formaId, @dzien, @godzRozp, @godzZak);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@przedmiotId", GetPrzedmiotId(z.Przedmiot));
            cmd.Parameters.AddWithValue("@prowadzacyId", GetProwadzacyId(z.Prowadzacy));
            cmd.Parameters.AddWithValue("@salaId", GetSalaId(z.Sala));
            cmd.Parameters.AddWithValue("@formaId", GetFormaId(z));
            cmd.Parameters.AddWithValue("@dzien", z.Dzien.ToString());
            cmd.Parameters.AddWithValue("@godzRozp", z.GodzinaRozpoczecia); // TimeSpan
            cmd.Parameters.AddWithValue("@godzZak", z.GodzinaZakonczenia);

            z.Id = (int)cmd.ExecuteScalar();

            if (z is Laboratorium lab)
            {
                InsertZajeciaGrupy(z.Id, new List<int> { lab.Grupa });
            }
            else if (z is Projekt proj)
            {
                InsertZajeciaGrupy(z.Id, new List<int> { proj.Grupa1, proj.Grupa2 });
            }
        }

        public void Delete(int zajeciaId)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using (var cmd = new SqlCommand("DELETE FROM Zajecia_Grupy WHERE ZajeciaId=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", zajeciaId);
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SqlCommand("DELETE FROM Zajecia WHERE ZajeciaId=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", zajeciaId);
                cmd.ExecuteNonQuery();
            }
        }

        private int GetPrzedmiotId(string nazwa)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "SELECT TOP 1 PrzedmiotId FROM Przedmiot WHERE Nazwa=@nazwa";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nazwa", nazwa);
            var res = cmd.ExecuteScalar();
            if (res == null) throw new ValidationException($"Nie znaleziono przedmiotu: {nazwa}");
            return (int)res;
        }

        private int GetProwadzacyId(string nazwiskoImie)
        {
            var parts = nazwiskoImie.Split(' ');
            if (parts.Length < 2) throw new ValidationException($"Nieprawidłowy prowadzący: {nazwiskoImie}");
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "SELECT TOP 1 ProwadzacyId FROM Prowadzacy WHERE Imie=@imie AND Nazwisko=@nazwisko";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@imie", parts[0]);
            cmd.Parameters.AddWithValue("@nazwisko", parts[1]);
            var res = cmd.ExecuteScalar();
            if (res == null) throw new ValidationException($"Nie znaleziono prowadzącego: {nazwiskoImie}");
            return (int)res;
        }

        private int GetSalaId(string sala)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "SELECT TOP 1 SalaId FROM Sala WHERE Nazwa=@sala";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@sala", sala);
            var res = cmd.ExecuteScalar();
            if (res == null) throw new ValidationException($"Nie znaleziono sali: {sala}");
            return (int)res;
        }

        private int GetFormaId(Zajecia z)
        {
            return z switch
            {
                Wyklad => 2,
                Laboratorium => 1,
                Projekt => 3,
                _ => throw new ValidationException("Nieznana forma zajęć")
            };
        }

        private List<int> GetGrupy(int zajeciaId)
        {
            var list = new List<int>();
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "SELECT GrupaId FROM Zajecia_Grupy WHERE ZajeciaId=@id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", zajeciaId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }
            return list;
        }

        private void InsertZajeciaGrupy(int zajeciaId, List<int> grupy)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            foreach (var g in grupy)
            {
                using var cmd = new SqlCommand("INSERT INTO Zajecia_Grupy (ZajeciaId, GrupaId) VALUES (@zaj, @grp)", conn);
                cmd.Parameters.AddWithValue("@zaj", zajeciaId);
                cmd.Parameters.AddWithValue("@grp", g);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
