using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class BankAccount
    {
        public string wlasciciel = "Roman Romanowski";
        public decimal saldo { get; private set; }
        public BankAccount() { }
        public BankAccount(string wlasciciel, decimal saldo) 
        {
            this.wlasciciel=wlasciciel;
            this.saldo = saldo;
        }
        public void Wplata(decimal kwota)
        {
            saldo = saldo + kwota;
            Console.WriteLine("wplacono" + kwota);
        }
        public void Wyplata(decimal kwota)
        {
            if (saldo > kwota) 
            {
                saldo = saldo - kwota;
            }
            Console.WriteLine("wplacono" + kwota);
        }
    }
    
}
