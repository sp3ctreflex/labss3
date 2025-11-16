using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Osoba
    {
        public string imie = "Roman";
        public string nazwisko = "Romanowski";
        public int wiek = 23;

        public Osoba() 
        {
            imie = "Roman";
            nazwisko = "Romanowski";
            wiek = 23;
        }
        public Osoba(string imie, string nazwisko, int wiek)
        {
            if (imie.Length < 2)
            {
                throw new ArgumentException("za krotkie imie");
            }
                this.imie = imie;
            if (string.IsNullOrWhiteSpace(nazwisko) || nazwisko.Length < 2)
            {
                throw new ArgumentException("za krotkie nazwisko");
            }
            this.nazwisko = nazwisko;
            if (wiek >= 0)
            {
                this.wiek = wiek;
            }
            else
            {
                Console.WriteLine("bledny wiek");
                this.wiek = 0;
            }
        }

        public void WyswietlInformacje() 
        {
            Console.WriteLine("Imie: " + imie + " nazwisko: " + nazwisko + " wiek: " + wiek);
        }

    }
}
