using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Biograf_Billetter
{
    internal class Order
    {
        public int quantity { get; set; }
        public int funds { get; set; }
        readonly int price = 120;

        int orderCount = 0;

        // File naming scheme:
        // order_{index}.txt

        // Some checking to see if we have any previous orders saved
        public void CheckOrders()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "orders");
            foreach (string file in Directory.EnumerateFiles(path, "*.txt"))
            {
                if (File.Exists(file))
                {
                    string fileName = Path.GetFileName(file);
                    string orderNum = fileName.Split("_")[1].Split(".")[0];
                    int.TryParse(orderNum, out int orderNumParsed);
                    if (orderNumParsed > orderCount) { orderCount = orderNumParsed; }
                }
            }
            Console.WriteLine(orderCount);
        }

        // Init
        public bool init(int f, int q)
        {
            if (q <= 0)
            {
                Console.WriteLine("You have to buy a minimum of 1 ticket.");
                Console.ReadKey();
                return false;
            }

            if (f <= 0)
            {
                Console.WriteLine("Your funds has to be greater than 0.");
                Console.ReadKey();
                return false;
            }

            if (f < q * price)
            {
                Console.WriteLine("Insufficient funds, please insert more funds before incresing ticket quantity.");
                Console.ReadKey();
                return false;
            }

            quantity = q;
            funds = f;

            return true;

        }

        // Change Quantity
        public bool ChangeQuantity(int q)
        {
            if (q <= 0)
            {
                Console.WriteLine("You have to buy a minimum of 1 ticket.");
                Console.ReadKey();
                return false;
            }

            if (funds < q * price)
            {
                Console.WriteLine("Insufficient funds, please insert more funds before incresing ticket quantity.");
                Console.ReadKey();
                return false;
            }

            quantity = q;
            return true;
        }

        public bool ChangeFunds(int f)
        {
            if (f <= 0)
            {
                Console.WriteLine("Your funds has to be greater than 0.");
                Console.ReadKey();
                return false;
            }

            if (f < quantity * price)
            {
                Console.WriteLine("Insufficient funds, please lower ticket amount before changing lowering funds");
                Console.ReadKey();
                return false;
            }

            funds = f;

            return true;
        }

        public void SaveOrder()
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "orders", $"order_{orderCount+1}.txt");

            string[] order =
            {
                (quantity*120).ToString(),
                (funds - quantity*120).ToString(),
                quantity.ToString(),
            };

            File.WriteAllLines(path, order);
        }
    }
}
