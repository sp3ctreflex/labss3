using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ContactManagerSQL.Data;
using ContactManagerSQL.Models;
using ContactManagerSQL.Utils;

class Program
{
    const string ConnectionString = "Server=10.200.2.28;Database=71335TestDB;Trusted_Connection=True;Encrypt=False;";

    static void Main()
    {
        var repo = new ContactRepository(ConnectionString);

        while (true)
        {
            PrintMenu();
            string choice = Console.ReadLine() ?? "";

            try
            {
                switch (choice)
                {
                    case "1": Create(repo); break;
                    case "2": ReadAll(repo); break;
                    case "3": Search(repo); break;
                    case "4": Update(repo); break;
                    case "5": Delete(repo); break;
                    case "6": BulkInsertDemo(repo); break;
                    case "0": return;
                    default: Console.WriteLine("Nieprawidłowy wybór."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("\n=== CONTACT MANAGER ===");
        Console.WriteLine("1) Dodaj kontakt");
        Console.WriteLine("2) Pokaż wszystkie");
        Console.WriteLine("3) Wyszukaj po nazwisku");
        Console.WriteLine("4) Edytuj kontakt");
        Console.WriteLine("5) Usuń kontakt");
        Console.WriteLine("6) Bulk insert (demo)");
        Console.WriteLine("0) Wyjście");
        Console.Write("Wybór: ");
    }

    // --- Helpers ---
    static string ReadRequired(string label)
    {
        while (true)
        {
            Console.Write(label);
            string s = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
            Console.WriteLine("Pole nie może być puste.");
        }
    }

    static string? ReadOptional(string label)
    {
        Console.Write(label);
        string s = Console.ReadLine() ?? "";
        return string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    }

    static int ReadInt(string label)
    {
        while (true)
        {
            Console.Write(label);
            if (int.TryParse(Console.ReadLine(), out int id)) return id;
            Console.WriteLine("Podaj poprawną liczbę całkowitą.");
        }
    }

    // --- CRUD ---
    static void Create(ContactRepository repo)
    {
        var c = new Contact
        {
            FirstName = ReadRequired("Imię: "),
            LastName = ReadRequired("Nazwisko: "),
            Phone = ReadOptional("Telefon: "),
            Email = ReadOptional("Email: ")
        };
        int id = repo.Add(c);
        Console.WriteLine($"Dodano kontakt z ID = {id}");
    }

    static void ReadAll(ContactRepository repo)
    {
        var list = repo.GetAll();
        Console.WriteLine("ID | Imię | Nazwisko | Telefon | Email");
        foreach (var c in list)
            Console.WriteLine(c);
    }

    static void Search(ContactRepository repo)
    {
        string part = ReadRequired("Fragment nazwiska: ");
        var results = repo.SearchByLastName(part);
        Console.WriteLine("ID | Imię | Nazwisko | Telefon | Email");
        foreach (var c in results)
            Console.WriteLine(c);
    }

    static void Update(ContactRepository repo)
    {
        int id = ReadInt("ID kontaktu do edycji: ");
        var c = new Contact
        {
            Id = id,
            FirstName = ReadRequired("Nowe imię: "),
            LastName = ReadRequired("Nowe nazwisko: "),
            Phone = ReadOptional("Nowy telefon: "),
            Email = ReadOptional("Nowy email: ")
        };
        bool updated = repo.Update(c);
        Console.WriteLine(updated ? "Kontakt zaktualizowany." : "Nie znaleziono kontaktu.");
    }

    static void Delete(ContactRepository repo)
    {
        int id = ReadInt("ID kontaktu do usunięcia: ");
        bool deleted = repo.Delete(id);
        Console.WriteLine(deleted ? "Kontakt usunięty." : "Nie znaleziono kontaktu.");
    }

    static void BulkInsertDemo(ContactRepository repo)
    {
        var list = new List<Contact>
        {
            new Contact { FirstName="Test1", LastName="Bulk", Phone="111", Email="t1@demo.com" },
            new Contact { FirstName="Test2", LastName="Bulk", Phone="222", Email="t2@demo.com" },
            new Contact { FirstName="Test3", LastName="Bulk", Phone="333", Email="t3@demo.com" },
        };
        int added = repo.BulkInsert(list);
        Console.WriteLine($"Dodano {added} rekordów.");
    }
}
