using ProductManager.Data;
using ProductManager.Domain;
using static System.Console;


namespace ProductManagerr;

class Program
{
    public static void Main()
    {
        Title = "Product Manager";

        while (true)
        {
            CursorVisible = false;

            WriteLine("1. Ny Produkt");
            WriteLine("2. Sök Produkt");
            WriteLine("3. Avsluta ");


            var keyPressed = ReadKey(true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    AddProductView();
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    SearchProductView();
                    break;
                                    
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    private static void AddProductView()
    {
        var name = GetUserInput("namn");
        var sku = GetUserInput("SKU");
        var description = GetUserInput("Beskrivning");
        var image = GetUserInput("Bild(URL)");
        var price = GetUserInput("Pris");


        var product = new Product 
        {
            Name = name,
            SKU = sku,
            Description = description,
            Image = image,
            Price = price
        };

        //save in Db 
        // create a Db-context
        SaveProduct(product);

        Clear();

        WriteLine("Product sparad");

        Thread.Sleep(2000);
    }

    private static void SearchProductView() // retunerar objekt av typ produkt

    {
         var sku = GetUserInput("SKU");

        var product = FindProduct(sku);

        if (product is not null)
        {
            WriteLine($"Namn: {product.Name}");
            WriteLine($"SKU: {product.SKU}");
            WriteLine($"Beskrivning: {product.Description}");
            WriteLine($"Bild: {product.Price}");
            WriteLine($"Pris: {product.Image}");
            WriteLine("(R)adera");
            var question = Console.ReadKey(true).Key;

            WaitUntilKeyPressed(ConsoleKey.Escape);

            Clear();

            if(question == ConsoleKey.R)
            {
               
                WriteLine ("Radera produkt? (J)a (N)ej");
                var confirmdelete = Console.ReadKey(true).Key;

                if(confirmdelete == ConsoleKey.J)
                {
                
                    using var context = new ApplicationDbContext();

                    context.Product.Remove(product);

                    context.SaveChanges();

                    WriteLine("Produkt raderad");
                }
                    
                 else if(question == ConsoleKey.N)
                 {
                  SearchProductView();  
                  
                 }

            }

        }

        else
        {
            WriteLine("Produktsaknas");

            Thread.Sleep(2000);
        }

    }
        private static Product? FindProduct(string sku)
    {
        using var context = new ApplicationDbContext();

        var product = context.Product.FirstOrDefault(x => x.SKU == sku);

        return product;
    }


    private static void SaveProduct(Product product)
    {
       using var context = new ApplicationDbContext();

        context.Product.Add(product);

        context.SaveChanges(); // save  the product in the DB
    }

        private static void WaitUntilKeyPressed(ConsoleKey key)
    {
        while (ReadKey(true).Key != key);
    }

    private static string GetUserInput(string label)
    {
        Write($"{label}: ");

        return ReadLine() ?? ""; //retunera nullable string tillbaka
    }
}