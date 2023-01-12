using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;

{
    {

        Console.WriteLine($"Prüfe die Tipps auf Duplikate...");
        var lottoTipp = new LottoTipp();
        lottoTipp.AddQuicktipps(1000);//1000
        for (int i = 0; i < 1000; i++)
        {
            var tipp = lottoTipp.GetTipp(i);
            if (tipp.Distinct().Count() != 6)
            {
                Console.Error.WriteLine($"FEHLER! Der Tipp {string.Join(",", tipp)} hat Duplikate!");
                return;
            }

            if (tipp.Max() > 45)
            {
                Console.Error.WriteLine($"FEHLER! Der Tipp {string.Join(",", tipp)} hat Zahlen über 45.");
                return;
            }

            if (tipp.Min() < 1)
            {
                Console.Error.WriteLine($"FEHLER! Der Tipp {string.Join(",", tipp)} hat Zahlen unter 1.");
                return;
            }
        }
    }

    {
        var lottoTipp = new LottoTipp();
        lottoTipp.AddQuicktipps(5);
        Console.WriteLine($"Generiere 5 Tipps...");
        for (int i = 0; i < 5; i++)
        {
            var tipp = lottoTipp.GetTipp(i);
            Console.WriteLine($"Tipp {i}: {string.Join(" ", tipp)}");
        }
    }

    {
        Console.WriteLine($"Generiere 1 000 000 Tipps und zähle die 6er, 5er, ...");
        var usedMemory = GC.GetTotalMemory(forceFullCollection: true);
        var lottoTipp = new LottoTipp();
        lottoTipp = new LottoTipp();
        lottoTipp.AddQuicktipps(1_000_000); //1_000_000
        var drawnNumbers = new int[] { 4, 2, 1, 8, 32, 16 };
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < 1_000_000; i++)
        {
            var rightNumbers = lottoTipp.CheckTipp(i, drawnNumbers);
            if (rightNumbers >= 5)
            {
                var tipp = lottoTipp.GetTipp(i);
                Console.WriteLine($"Tipp {i:000 000} hat {rightNumbers} Richtige: {string.Join(" ", tipp)}");

            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Berechnung nach {stopwatch.ElapsedMilliseconds} ms beendet.");
        Console.WriteLine($"{(GC.GetTotalMemory(forceFullCollection: true) - usedMemory) / 1048576M:0.00} MBytes belegt.");
    }
    {
        {
            var lottoTipp = new LottoTipp();
            try
            {
                lottoTipp.GetTipp(-1);
            }
            catch
            {

            }
        }
    }
}

// TODO: Implementiere die Klasse. Füge notwendige Properties und interne Listen hinzu.
class LottoTipp
{
    private readonly Random _random = new Random(906); // Fixed Seed, erzeugt immer die selbe Sequenz an Werten.
    private List<int[]> tipps = new List<int[]>();
    private List<List<int>> Tippliste = new();
    private int[,] Tippliste2;

    /// <summary>
    /// Property; Gibt die Anzahl der gespeicherten Tipps zurück.
    /// </summary>
    public int TippCount => tipps.Count; 

    /// <summary>
    /// Gibt den nten gespeicherten Tipp als Array zurück. Der erste Tipp hat die Nummer 0.
    /// </summary>
    public int[] GetTipp(int number)
    {
        return tipps[number];

        //List<int> numbs = new List<int>();
        //int[] numbs = new int[6];
        //if (number > Tippliste.Count)
        //{
        //    new ArgumentOutOfRangeException();
        //}

        //else
        //{
        //    for (int i = 0; i < Tippliste[number].Count; i++)
        //    {
        //        numbs[i] = Tippliste[number][i];
        //    }

        //}
        //return numbs;
    }

    /// <summary>
    /// Generiert 6 zufällige Zahlen zwischen 1 und 45 ohne Kollision.
    /// </summary>
    public int[] GetNumbers()
    {
        int[] randomNumbs = new int[6];

        for (int i = 0; i < 6; i++)
        {
            int number = _random.Next(1, 46);
            bool contains = false;
            for (int j = 0; j < randomNumbs.Length; j++)
            {
                if (number == randomNumbs[j])
                {
                    contains = true;
                }
            }

            if (contains)
            {
                i--;
            }
            else
            {
                randomNumbs[i] = number;
            }


        //    List<int> randomNumbs = new List<int>(6);

        //for (int i = 0; i < 6; i++)
        //{
        //    int number = _random.Next(1, 46);
        //    bool contains = false;
        //    for (int j = 0; j < randomNumbs.Count; j++)
        //    {
        //        if (number == randomNumbs[j])
        //        {
        //            contains = true;
        //        }
        //    }

        //    if (contains)
        //    {
        //        i--;
        //    }
        //    else
        //    {
        //        randomNumbs.Add(number);
        //    }
            //do {number = _random.Next();}
            //while (randomNumbs.Any(x=> x == number));

        }


        return randomNumbs;
    }

    

/// <summary>
    /// Fügt die in count übergebene Anzahl an Tipps zur internen Tippliste hinzu.
    /// </summary>
    /// <param name="count"></param>
    public void AddQuicktipps(int count)
    {
        for (int i = 0; i < count; i++)
        {
            tipps.Add(GetNumbers());
        }
        //Tippliste2 = new int[count,6];

        //if (count != 0)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        //Tippliste.Add(GetNumbers());
        //        Tippliste2[i,i] = GetNumbers();
        //    }
        //}
        //else
        //{
        //    new ArgumentException("count is 0");
        //}



        //for (int i = 0; i < count; i++)
        //{
        //    List<int> randomNumbs = GetNumbers();
        //    bool contains = false;

        //    if (Tippliste.Count == 0)
        //    {
        //        Tippliste.Add(randomNumbs);
        //    }
        //    else
        //    {
        //        foreach (var tipp in Tippliste)
        //        {
        //            if (!tipp.Except(randomNumbs).Any())
        //            {
        //                contains = true;
        //                break;
        //            }
        //        }

        //        if (contains)
        //        {
        //            i--;
        //        }
        //        else
        //        {
        //            Tippliste.Add(randomNumbs);
        //        }

        //    }


        //}
    
    }

    //public static bool ContainsAll<int>(IEnumerable<int> containingList, IEnumerable<int> lookupList)
    //{
    //    return !lookupList.Except(containingList).Any();
    //}

    /// <summary>
    /// Prüft, wie viele Richtige der nte Tipp hat. Die Tippnummer beginnt bei 0
    /// (0 ist also der erste Tipp, ...).
    /// </summary>
    public int CheckTipp(int tippNr, int[] drawnNumbers)
    {
        int result = 0;
        foreach(var item in drawnNumbers)
            if (GetTipp(tippNr).Any(x => x == item))
                result++;
        return result;

        //int ans=0;
        //List<int> drawnNumbs = drawnNumbers.ToList();

        //for (int i = 0; i < drawnNumbers.Length; i++)
        //{
        //    if (drawnNumbs.Contains(Tippliste[tippNr][i]))
        //    {
        //        ans++;
        //    }
        //}

        //if (!tipp.Except(drawnNumbs).Any())
        //{
        //    ans++;
        //}


        //return ans;
    }

    //Prüfe die Tipps auf Duplikate...
    //Generiere 5 Tipps...
    //Tipp 0: 2 30 3 43 12 14
    //Tipp 1: 39 44 3 17 35 36
    //Tipp 2: 21 37 8 39 32 33
    //Tipp 3: 10 6 9 5 25 23
    //Tipp 4: 12 40 3 36 34 30
    //Generiere 1 000 000 Tipps und zähle die 6er und 5er. //  4, 2, 1, 8, 32, 16
    //    Tipp 000 094 hat 5 Richtige: 2 4 27 16 8 32
    //Tipp 017 533 hat 5 Richtige: 8 41 4 1 2 16
    //Tipp 065 810 hat 5 Richtige: 2 18 16 4 32 1
    //Tipp 111 809 hat 5 Richtige: 2 4 1 32 16 9
    //Tipp 137 467 hat 5 Richtige: 8 4 16 32 11 2
    //Tipp 196 819 hat 5 Richtige: 16 2 8 4 29 1
    //Tipp 248 287 hat 5 Richtige: 8 2 1 32 16 31
    //Tipp 288 697 hat 5 Richtige: 28 8 4 1 2 32
    //Tipp 324 754 hat 5 Richtige: 13 4 1 2 8 16
    //Tipp 436 717 hat 5 Richtige: 8 16 32 2 4 11
    //Tipp 473 618 hat 5 Richtige: 1 2 8 16 32 19
    //Tipp 477 288 hat 5 Richtige: 9 32 8 16 1 4
    //Tipp 499 182 hat 6 Richtige: 16 1 32 8 4 2
    //Tipp 519 778 hat 5 Richtige: 2 8 4 32 31 1
    //Tipp 529 366 hat 5 Richtige: 2 4 32 36 8 16
    //Tipp 585 261 hat 5 Richtige: 4 20 1 2 16 32
    //Tipp 680 855 hat 5 Richtige: 37 32 1 4 16 2
    //Tipp 707 693 hat 5 Richtige: 16 43 4 32 8 1
    //Tipp 738 554 hat 5 Richtige: 30 1 16 8 32 2
    //Tipp 770 300 hat 5 Richtige: 2 16 39 32 8 4
    //Tipp 784 975 hat 5 Richtige: 2 16 32 8 20 1
    //Tipp 807 334 hat 5 Richtige: 1 37 16 4 8 2
    //Tipp 911 569 hat 5 Richtige: 32 2 8 4 45 1
    //Tipp 916 819 hat 5 Richtige: 8 10 16 32 1 2
    //Tipp 924 592 hat 5 Richtige: 8 4 1 38 2 32
    //Tipp 942 173 hat 5 Richtige: 1 2 8 32 16 12
    //Tipp 985 945 hat 5 Richtige: 16 8 2 4 32 14
    //Berechnung nach 81 ms beendet.
    //53,78 MBytes belegt.


}
