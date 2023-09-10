using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Biograf_Billetter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();
            order.CheckOrders();

            bool main = true;

            // Main loop
            do
            {
                Console.Clear();
                Console.WriteLine("Please enter your funds.");
                if(!int.TryParse(Console.ReadLine(), out int funds))
                {
                    Console.WriteLine("Funds invalid, make sure the number is greater than 0");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine("Please enter ticket quantity");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Quantity invalid, make sure the number is greater than 0");
                    Console.ReadKey();
                    continue;
                }

                if(order.init(funds, quantity))
                {
                    main = false;
                }

            } while (main);

            // Giving the opportunity to change their funds and ticket quantity
            bool changes = true;
            do
            {
                Console.Clear();
                Console.WriteLine("What do you want to do next?\n" +
                    "1. Change funds\n" +
                    "2. Change ticket quantity\n" +
                    "3. Confirm order");
                if(!int.TryParse (Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a number between 1 - 3");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please enter new funds");
                        if(!int.TryParse(Console.ReadLine(), out int funds))
                        {
                            Console.WriteLine("Funds invalid, make sure the number is greater than 0");
                            Console.ReadKey();
                            continue;
                        }
                        if (!order.ChangeFunds(funds)) continue;
                        Console.WriteLine("Changed funds");
                        break;

                    case 2:
                        Console.WriteLine("Please enter new ticket quantity");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Ticket quantity invalid, make sure the number is greater than 0");
                            Console.ReadKey();
                            continue;
                        }
                        if (!order.ChangeQuantity(quantity)) continue;
                        Console.WriteLine("Changed quantity");
                        break;

                    case 3:
                        Console.WriteLine($"Price: {order.quantity*120}\n" +
                            $"Remaining money: {order.funds - order.quantity*120}\n" +
                            $"Ticket Quantity: {order.quantity}");
                        order.SaveOrder();
                        changes = false;
                        break;
                }

            } while (changes);

            Console.ReadKey();
            
        }
    }
}