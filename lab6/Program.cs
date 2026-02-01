using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            var txtRepo = new TxtContactRepository("contacts.txt");
            var jsonRepo = new JsonContactRepository("contacts.json");

            List<Contact> contacts = txtRepo.Load();

            while (true)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Wyświetl kontakty");
                Console.WriteLine("2. Dodaj kontakt");
                Console.WriteLine("3. Modyfikuj kontakt");
                Console.WriteLine("4. Usuń kontakt");
                Console.WriteLine("5. Zapisz do TXT");
                Console.WriteLine("6. Zapisz do JSON");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybór: ");


                switch (Console.ReadLine())
                {
                    case "1":
                        foreach (var c in contacts)
                            Console.WriteLine(c);
                        break;

                    case "2":
                        AddContact(contacts);
                        break;

                    case "3":
                        EditContact(contacts);
                        break;

                    case "4":
                        DeleteContact(contacts);
                        break;

                    case "5":
                        txtRepo.Save(contacts);
                        Console.WriteLine("Zapisano do TXT.");
                        break;

                    case "6":
                        jsonRepo.Save(contacts);
                        Console.WriteLine("Zapisano do JSON.");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
        }

        static void AddContact(List<Contact> contacts)
        {
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Imię i nazwisko: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            contacts.Add(new Contact
            {
                Id = id,
                Name = name,
                Email = email
            });
        }
        static void EditContact(List<Contact> contacts)
        {
            Console.Write("Podaj Id kontaktu do modyfikacji: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Błędne Id.");
                return;
            }

            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                Console.WriteLine("Nie znaleziono kontaktu.");
                return;
            }

            Console.Write($"Nowe imię i nazwisko ({contact.Name}): ");
            string name = Console.ReadLine();
            Console.Write($"Nowy email ({contact.Email}): ");
            string email = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
                contact.Name = name;

            if (!string.IsNullOrWhiteSpace(email))
                contact.Email = email;

            Console.WriteLine("Kontakt zmodyfikowany.");
        }

        static void DeleteContact(List<Contact> contacts)
        {
            Console.Write("Podaj Id kontaktu do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Błędne Id.");
                return;
            }

            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                Console.WriteLine("Nie znaleziono kontaktu.");
                return;
            }

            contacts.Remove(contact);
            Console.WriteLine("Kontakt usunięty.");
        }
    }
}