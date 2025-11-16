using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Student
    {
        public string imie = "Roman";
        public string nazwisko = "Romanowski";
        private List<double> tablicaocen = new List<double>();
        public Student()
        {
            this.imie = "Adam";
            this.nazwisko = "Nowak";
        }
        public Student(string imie, string nazwisko, List<double> tablicaocen   )
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.tablicaocen = tablicaocen;
        }
        public void DodajOcene(int ocena)
        {
            tablicaocen.Add(ocena);
        }
        
        public double SredniaOcen
        {
          get
          {
                Console.WriteLine("sredn ocen ");
                return tablicaocen.Average();
            }
        set
          {
          }
        }
    }
}
