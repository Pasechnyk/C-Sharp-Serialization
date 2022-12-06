using System;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Serialization
{
    // Task 2-4 - create a manager app for magazines using serialization

    [Serializable]
    public class Article
    {
        // properties
        public string Name { get; set; }
        public int SymbolsCount { get; set; }
        public double ReleaseDate { get; set; }

        // constructors
        public Article() { }
        public Article(string name, int symbols, double release)
        {
            Name = name;
            SymbolsCount = symbols;
            ReleaseDate = release;
        }
    }

    [Serializable]
    public class Magazine
    {
        // properties
        public string Title { get; set; }
        public string Publisher { get; set; }
        public double PublishTime { get; set; }
        public int PageCount { get; set; }
        public List<Article> articles { get; set; }

        // constructors
        public Magazine() { }
        public Magazine(string title,string pub, double time, int page)
        {
            Title = title;
            Publisher = pub;
            PublishTime = time;
            PageCount = page;

            Article article = new Article();
        }

        // Show and ToString method
        public void ShowInfo()
        {
            Console.WriteLine(" ------------ Information about given magazine ------------ ");
            Console.WriteLine($"Title of magazine: {Title}");
            Console.WriteLine($"Publisher name: {Publisher}");
            Console.WriteLine($"Pages count is - {PageCount}");
        }
        public override string ToString()
        {
            return $"Name: {Title}, Publisher: {Publisher}, Date of publishing: {PublishTime}," +
                $"Page count: {PageCount} .";
        }
    }

    [Serializable]
    public class AppManager
    {
        // list property
        public List<Magazine> magazines { get; set; }

        // methods
        public void EnterNewMagazine()
        {
            Console.WriteLine("Please enter the title to your new magazine to make a record: ");
            string str = Console.ReadLine(); 
        }
        public void ShowInfo()
        {
            foreach (var m in magazines)
            {
                Console.WriteLine(m);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // initialization and serialization 
            AppManager manager = new AppManager();
            BinaryFormatter bin = new BinaryFormatter();

            // magazines and articles collections
            List<Magazine> magazine = new Magazine()
            {
                new Magazine("VOGUE", "Nature America", 12.05, 53),
                new Magazine("Cosmopolitan", "Nature America", 08.07, 60)
            };

            List<Article> article = new Article()
            {
                new Article("Modern Art", 7250, 02.12),
                new Article("Fate of Gucci", 5890, 04.12),
                new Article("Resurrection of expression and uniqueness ", 6244, 10.12)
            };

            // menu with switch
            Console.WriteLine("------------ MAGAZINES MANAGEMENT APP ------------");
            Console.WriteLine("Choose your option: \n 1 - Register new magazine title " +
                "\n 2 - Show available articles" +
                "\n 3 - Save magazines into the file " +
                "\n 4 - Load information about magazine");
            int option;
            option = Console.Read();
            switch (option)
            {
                case 1:
                    manager.EnterNewMagazine();
                    break;
                case 2:
                    manager.ShowInfo();
                    break;
                case 3:
                    {
                        using (Stream fStream = File.Create("magazine.bin"))
                        {
                            bin.Serialize(fStream, magazine);
                        }
                        Console.WriteLine("Serialization is complete!");
                    }
                    break;
                case 4:
                    {
                        Magazine mag = null;
                        using (Stream fStream = File.OpenRead("magazine.bin"))
                        {
                            mag = (Magazine)bin.Deserialize(fStream);
                        }
                        Console.WriteLine(mag);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
