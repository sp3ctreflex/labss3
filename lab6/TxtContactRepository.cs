using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    internal class TxtContactRepository
    {
        private readonly string _path;

        public TxtContactRepository(string path)
        {
            _path = path;
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_path))
                File.WriteAllText(_path, "");
        }

        public List<Contact> Load()
        {
            var contacts = new List<Contact>();
            var lines = File.ReadAllLines(_path);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length != 3) continue;

                if (!int.TryParse(parts[0], out int id)) continue;

                contacts.Add(new Contact
                {
                    Id = id,
                    Name = parts[1],
                    Email = parts[2]
                });
            }
            return contacts;
        }

        public void Save(List<Contact> contacts)
        {
            using StreamWriter sw = new StreamWriter(_path);
            foreach (var c in contacts)
            {
                sw.WriteLine($"{c.Id};{c.Name};{c.Email}");
            }
        }
    }
}