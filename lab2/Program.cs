// See https://aka.ms/new-console-template for more information
using ConsoleApp3;

//Console.WriteLine("Hello, World!");
//Car car = new Car(); // tworzenie obiektu klasy
Osoba osoba = new Osoba("Jarek","Jarkowski",20);
//Console.WriteLine("Samochod kolor: " + car.color + " rocznik: " + car.rok);
osoba.WyswietlInformacje();

BankAccount konto = new BankAccount("Jan Kowalski", 1000);
konto.Wplata(500);
konto.Wyplata(200);
//konto.saldo = 220;
Console.WriteLine($"Saldo: {konto.saldo}");

Student student = new Student("Adam", "Nowak",[2,3,4]);
student.DodajOcene(4);
Console.WriteLine(student.SredniaOcen);

Licz a = new Licz(10);
a.Wyswietl();
a.Dodaj(5);
a.Wyswietl();

Sumator s = new Sumator([1, 2, 3, 4, 5, 6]);
s.Suma();
s.IleElementów();
s.WypiszElemTab();
s.WypiszZakres(-3, 100);