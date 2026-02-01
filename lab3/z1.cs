using System;
using System.Collections.Generic;

namespace LibraryLab
{
    
    public class Person
    {
        
        private string firstName;
        private string lastName;
        private int age;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public int Age { get { return age; } set { age = value; } }

        public Person(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }

        pochodnych
        public virtual void View()
        {
            Console.WriteLine($"Osoba: {FirstName} {LastName}, wiek: {Age}");
        }
    }

    public class Book
    {
        
        protected string title;
        protected Person author;
        protected DateTime releaseDate;

        public string Title { get { return title; } set { title = value; } }
        public Person Author { get { return author; } set { author = value; } }
        public DateTime ReleaseDate { get { return releaseDate; } set { releaseDate = value; } }

        public Book(string title, Person author, DateTime releaseDate)
        {
            this.title = title;
            this.author = author;
            this.releaseDate = releaseDate;
        }

        public void View()
        {
            Console.WriteLine($"Książka: {Title}, autor: {Author.FirstName} {Author.LastName}, data wydania: {ReleaseDate.ToShortDateString()}");
        }
    }

    
    public class Reader : Person
    {
        private List<Book> readBooks; użyjemy metody ViewBook do dostępu

        public Reader(string firstName, string lastName, int age) : base(firstName, lastName, age)
        {
            readBooks = new List<Book>();
        }

        public void AddBook(Book book)
        {
            readBooks.Add(book);
        }

        public void ViewBook()
        {
            Console.WriteLine("Przeczytane książki:");
            foreach (var book in readBooks)
            {
                book.View();
            }
        }

        
        public override void View()
        {
            base.View();
            ViewBook();
        }
    }

    
    public class Reviewer : Reader
    {
        private static Random rnd = new Random();

        public Reviewer(string firstName, string lastName, int age) : base(firstName, lastName, age) { }

        
        public void ViewBookWithRating()
        {
            Console.WriteLine("Przeczytane książki z ocenami:");
            foreach (var book in GetBooks()) pomocniczej do odczytu książek
            {
                int rating = rnd.Next(1, 6); 
                Console.WriteLine($"{book.Title} - ocena: {rating}");
            }
        }

        readBooks jest private)
        private List<Book> GetBooks()
        {
            bezpośrednio odczytać, więc możemy użyć metody ViewBook
            np:
            var type = typeof(Reader);
            var field = type.GetField("readBooks", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (List<Book>)field.GetValue(this);
        }

        public override void View()
        {
            base.View();
            ViewBookWithRating();
        }
    }

    
    public class AdventureBook : Book
    {
        public string Setting { get; set; }

        public AdventureBook(string title, Person author, DateTime releaseDate, string setting) 
            : base(title, author, releaseDate)
        {
            Setting = setting;
        }

        public void ViewAdventure()
        {
            Console.WriteLine($"Książka przygodowa: {Title}, miejsce akcji: {Setting}");
        }
    }

    public class DocumentaryBook : Book
    {
        public string Topic { get; set; }

        public DocumentaryBook(string title, Person author, DateTime releaseDate, string topic)
            : base(title, author, releaseDate)
        {
            Topic = topic;
        }

        public void ViewDocumentary()
        {
            Console.WriteLine($"Książka dokumentalna: {Title}, temat: {Topic}");
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            
            Person author1 = new Person("Adam", "Mickiewicz", 45);
            Book book1 = new Book("Pan Tadeusz", author1, new DateTime(1834, 6, 28));
            book1.View();

            
            Reader reader1 = new Reader("Jan", "Kowalski", 25);
            Reader reader2 = new Reader("Anna", "Nowak", 30);

            reader1.AddBook(book1);
            Book book2 = new Book("Krzyżacy", new Person("Henryk", "Sienkiewicz", 60), new DateTime(1900, 1, 1));
            reader1.AddBook(book2);

            reader2.AddBook(book2);

            
            reader1.View();
            reader2.View();

            
            Reviewer rev1 = new Reviewer("Tomasz", "Wiśniewski", 40);
            rev1.AddBook(book1);
            rev1.AddBook(book2);

            rev1.View();

            
            List<Person> people = new List<Person>() { reader1, rev1, new Person("Piotr", "Zieliński", 20) };
            Console.WriteLine("\nWszystkie osoby:");
            foreach (var p in people)
            {
                p.View();
                Console.WriteLine();
            }

            
            AdventureBook advBook = new AdventureBook("Przygoda w dżungli", author1, new DateTime(2020, 5, 1), "Amazonia");
            DocumentaryBook docBook = new DocumentaryBook("Historia Polski", author1, new DateTime(2019, 3, 10), "Polska");

            reader1.AddBook(advBook);
            reader2.AddBook(docBook);

            Console.WriteLine("\nNowe książki:");
            advBook.ViewAdventure();
            docBook.ViewDocumentary();
        }
    }
}
