using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fractions
{
    // Task 1 - create fraction system with serialization

    [Serializable]
    public class Fraction
    {
        // fraction properties 
        public int Numerator { get; set; }
        public int Denotator { get; set; }
        public List<Fraction> fractions;

        // constructors
        public Fraction() { }
        public Fraction(int num, int den)
        {
            Numerator = num;
            Denotator = den;
        }

        public void ShowList()
        {
            foreach (var f in fractions)
            {
                Console.Write(f);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // initialize list of fractions

            Fraction f = new Fraction();
            List<Fraction> fractions = new List<Fraction>()
            {
                new Fraction(1, 3),
                new Fraction(2, 5),
                new Fraction(1, 4),
                new Fraction(6, 7)
            };
            
            f.ShowList();

            // serialization
            BinaryFormatter bin = new BinaryFormatter();
            try
            {
                using (Stream fStream = File.Create("Fraction.bin"))
                {
                    bin.Serialize(fStream, f);
                }
                Console.WriteLine("Serialization was completed!");

                Fraction frac = null;
                using (Stream fStream = File.OpenRead("Fraction.bin"))
                {
                    frac = (Fraction)bin.Deserialize(fStream);
                }
                Console.WriteLine(frac);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
