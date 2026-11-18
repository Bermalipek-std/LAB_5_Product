using System;
using System.Globalization;
using System.Linq;

public class Product
{
    private double price;

    public string ProductCode { get; }
    public string Name { get; set; }
    public int Stock { get; private set; }

    public double Price
    {
        get
        {
            return price;
        }
        set
        {
            if (value > 0)
            {
                price = value;
            }
            else
            {
                Console.WriteLine($"Invalid price ({value})");
                price = 1.0;
            }
        }
    }

    public Product(string productCode, string name, double price, int initialStock)
    {
        ProductCode = productCode;
        Name = name;
        Price = price;
        Stock = initialStock;
    }

    public void Restock(int amount)
    {
        if (amount > 0)
        {
            Stock += amount;
        }
    }

    public string info()
    {
        return FormattableString.Invariant(
            $"[{ProductCode}] {Name}-${Price:F2}(Stock:{Stock})");
    }
}

public class Program
{
    public static void Main()
    {
        
        Product[] products = new Product[4]
        {
            new Product("P101", "Wireless Mouse", 25.50, 15),
            new Product("P102", "Mechanical Keyboard", 85.00, 10),
            new Product("P103", "Wireless Mouse", -10.00, 50),
            new Product("P104", "Wireless Mouse", 220.00, 5)
        };

        products[0].Restock(5);
        products[0].Restock(-3);

        
        foreach (Product product in products)
        {
            Console.WriteLine(product.info());
        }

        
        double[] prices = products.Select(product => product.Price).ToArray();

        Console.WriteLine();
        Console.WriteLine($"Minimum price: ${prices.Min().ToString("F2", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Maximum price: ${prices.Max().ToString("F2", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Average price: ${prices.Average().ToString("F2", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Products priced $50 or more: {prices.Count(p => p >= 50)}");

        
        double[] sortedPrices = (double[])prices.Clone();
        Array.Sort(sortedPrices);

        Console.WriteLine();
        Console.WriteLine("Sorted prices:");

        foreach (double sortedPrice in sortedPrices)
        {
            Console.WriteLine($"${sortedPrice.ToString("F2", CultureInfo.InvariantCulture)}");
        }

        double targetPrice = 85.00;
        string formattedTarget = targetPrice.ToString("F2", CultureInfo.InvariantCulture);
        int index = Array.BinarySearch(sortedPrices, targetPrice);

        if (index >= 0)
        {
            Console.WriteLine($"${formattedTarget} found at index {index}.");
        }
        else
        {
            Console.WriteLine($"${formattedTarget} was not found.");
        }
    }
}