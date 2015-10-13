using System;
using System.Collections.Generic;
using System.Linq;

public class Patent
{
    public string Title { get; set; }

    public string YearOfPublication { get; set; }

    public string ApplicationNumber { set; get; }

    public long[] InventorIds { get; set; }

    public override string ToString()
    {
        return string.Format("{0}({1})", Title, YearOfPublication);
    }
}

public class Inventor
{
    public long Id { get; set; }
    public string Name { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public override string ToString()
    {
        return string.Format("{0}({1}, {2})", Name, City, State);
    }
}

public static class PatentData
{
    public static readonly Inventor[] Inventors = new Inventor[]
    {
        new Inventor(){Name = "franklin", City="philadelphia", State ="PA", Country="USA", Id=1},
        new Inventor(){Name = "orville wright", City="Kitty hawk", State ="NC", Country="USA", Id=2},
        new Inventor(){Name = "wilbur wright", City="Kitty hawk", State ="NY", Country="USA", Id=3},
        new Inventor(){Name = "saumel morese", City="new york", State ="NY", Country="USA", Id=4},
        new Inventor(){Name = "stephenson", City="wylam", State ="northumbedrland", Country="UK", Id=5},
        new Inventor(){Name = "john", City="Chicago", State ="IL", Country="USA", Id=6},
        new Inventor(){Name = "mary", City="new york", State ="PA", Country="USA", Id=7},
    };

    public static readonly Patent[] Patents = new Patent[]
    {
        new Patent(){Title = "bifocals", YearOfPublication = "1784", InventorIds = new long[]{1}},
        new Patent(){Title = "phonogrhph", YearOfPublication = "1877", InventorIds = new long[]{1}},
        new Patent(){Title = "kinet", YearOfPublication = "1888", InventorIds = new long[]{1}},
        new Patent(){Title = "electrical", YearOfPublication = "1847", InventorIds = new long[]{4}},
        new Patent(){Title = "steam", YearOfPublication = "1815", InventorIds = new long[]{5}},
        new Patent(){Title = "flying", YearOfPublication = "1903", InventorIds = new long[]{2,3}},
        new Patent(){Title = "backless", YearOfPublication = "1915", InventorIds = new long[]{7}},
        new Patent(){Title = "droplet", YearOfPublication = "1989", InventorIds = new long[]{6}},
    };
}

class LinqTest
{
    private static void Print<T>(IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            Console.WriteLine(item);
        }
    }

    static public void Test()
    {
        IEnumerable<Patent> patents = PatentData.Patents;
        Print(patents);

        Console.WriteLine();

        IEnumerable<Inventor> inventors = PatentData.Inventors;
        Print(inventors);

        Console.WriteLine("------------query year start with 18---------");

        patents = patents.Where(patent => { Console.WriteLine("querying..."); 
            return patent.YearOfPublication.StartsWith("18"); });
        Console.WriteLine("----print---");
        Print(patents);

        IEnumerable<string> items = patents.Select(patent => patents.ToString());
        Console.WriteLine("-------ToString------");
        Print(items);
        Console.WriteLine("-------cout------");
        Console.WriteLine(items.Count());

        Console.WriteLine("-----------ToArray----------");
        items = items.ToArray();
        Console.WriteLine("-----------Print----------");
        Console.WriteLine(items.Count());
    }
}
