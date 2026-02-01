using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Internship : IContract
    {
        public decimal StawkaMiesieczna {  get; set; }

        //public Internship() : this(1000m) { }

        public Internship(decimal stawkaMiesieczna)
        {
            StawkaMiesieczna = stawkaMiesieczna;
        }

        public decimal Salary()
        {
            return StawkaMiesieczna;
        }

        public override string? ToString()
        {
            //return base.ToString();
            return $"Umowa:, staz: stawka: ({StawkaMiesieczna.ToString("C", new CultureInfo("pl-PL"))})";
        }
    }
}
