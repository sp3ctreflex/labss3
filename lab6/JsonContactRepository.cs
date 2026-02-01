using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class JsonContactRepository
    {
        private readonly string _path ;

        public JsonContactRepository(string path)
        {
            _path = path;
        }
        public List<Contact> Load()
        {
            if (!File.Exists(_path))
                return new List<Contact>();

            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Contact>>(json)
                   ?? new List<Contact>();
        }

        public void Save(List<Contact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_path, json);
        }
    }
}
