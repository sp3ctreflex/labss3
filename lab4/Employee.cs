using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Employee :IContract
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IContract Contract { get; private set; }

        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Contract = 
        }

        public Employee(string firstName, string lastName, IContract contract) : this(firstName, lastName)
        {
            Contract = contract;
        }
        void ZmienKontrakt(IContract nowyKontrakt) 
        {
            Contract = nowyKontrakt;
        }
        public IContract Pensja()
        {
            return Contract;
        }
        public override string? ToString()
        {
            //return base.ToString();
            return $"Imie nazwisko ({FirstName}+{LastName} + wynagrodzenie: {Contract.ToString("C", new CultureInfo("pl-PL"))})";
        }
    }
}
