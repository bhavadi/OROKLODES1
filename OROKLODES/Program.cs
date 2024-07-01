using System;
using System.Collections.Generic;
using System.IO;

public abstract class Allat
{
    public static int MaxKor = 10;
    private static int KovId = 1;
    public int Id { get; private set; }
    public string Nev { get; private set; }
    public int SzulEv { get; private set; }
    public int SzepsegPontok { get; set; }
    public int ViselkedesPontok { get; set; }

    protected Allat(string nev, int szulEv)
    {
        Id = KovId++;
        Nev = nev;
        SzulEv = SzulEv;
    }

    public int Kor => DateTime.Now.Year - SzulEv;

    public abstract int Eredmeny();

    public override string ToString()
    {
        return $"{Id}, {this.GetType().Nev} {Nev}, {Eredmeny()}";
    }
}

public class Kutya : Allat
{
    public int RelationshipPoints { get; set; }

    public Kutya(string Nev, int szulEv, int viselkedesPontok) : base(Nev, szulEv)
    {
        ViselkedesPontok = viselkedesPontok;
    }

    public override int Eredmeny()
    {
        if (RelationshipPoints == 0)
            return 0;

        int ageFactor = MaxKor - Kor;
        if (Kor > MaxKor)
            return 0;

        return ageFactor * SzepsegPontok + Kor * SzepsegPontok + RelationshipPoints;
    }
}

public class Macska : Allat
{
    public bool HasCarrier { get; set; }

    public Macska(string Nev, int birthYear, bool hasCarrier) : base(Nev, birthYear)
    {
        HasCarrier = hasCarrier;
    }

    public override int Eredmeny()
    {
        if (!HasCarrier)
            return 0;

        int ageFactor = MaxKor - Kor;
        if (Kor > MaxKor)
            return 0;

        return ageFactor * SzepsegPontok + Kor * SzepsegPontok;
    }
}

class Program
{
    static void Main()
    {
        List<Allat> allatok = new List<Allat>();

        // Adatok beolvasása fájlból
        string[] lines = File.ReadAllLines("allatok.txt");
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts[0] == "Dog")
            {
                allatok.Add(new Kutya(parts[1], int.Parse(parts[2]), int.Parse(parts[5]))
                {
                    SzepsegPontok = int.Parse(parts[3]),
                    ViselkedesPontok = int.Parse(parts[4])
                });
            }
            else if (parts[0] == "Cat")
            {
                allatok.Add(new Macska(parts[1], int.Parse(parts[2]), bool.Parse(parts[5]))
                {
                    SzepsegPontok = int.Parse(parts[3]),
                    ViselkedesPontok = int.Parse(parts[4])
                });
            }
        }

        // Adatok kiírása regisztráció után
        Console.WriteLine("Regisztráció után:");
        foreach (Allat Allat in allatok)
        {
            Console.WriteLine(allatok);
        }

        // Verseny lebonyolítása (pontszámok kiszámítása)
        Console.WriteLine("\nVerseny után:");
        foreach (Allat Allat in allatok)
        {
            Console.WriteLine(allatok);
        }
    }
}