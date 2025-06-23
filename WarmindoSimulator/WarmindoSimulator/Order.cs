using System;
using System.Collections.Generic;
using System.Text;

namespace WarmindoSimulator
{
    public class Order
    {
        public Dictionary<string, int> MieOrders { get; private set; }
        public Dictionary<string, int> DrinkOrders { get; private set; }

        private Random rand = new Random();

        private string[] mieMenu = { "Mie Goreng", "Mie Kuah", "Mie + Telur"};
        private string[] drinkMenu = { "Teh Manis", "Air Putih", "Es Jeruk", "Kopi Hitam" };

        public Order()
        {
            MieOrders = new Dictionary<string, int>();
            DrinkOrders = new Dictionary<string, int>();
            GenerateRandomOrder();
        }

        private void GenerateRandomOrder()
        {
            // Random jenis mie yang dipesan (1-2 jenis)
            int mieTypeCount = rand.Next(1, 3);
            for (int i = 0; i < mieTypeCount; i++)
            {
                string mie = mieMenu[rand.Next(mieMenu.Length)];
                int qty = rand.Next(1, 4); // 1 sampai 3 porsi
                if (!MieOrders.ContainsKey(mie))
                    MieOrders[mie] = qty;
            }

            // Random jenis minuman (1 jenis)
            int drinkTypeCount = 1;
            for (int i = 0; i < drinkTypeCount; i++)
            {
                string drink = drinkMenu[rand.Next(drinkMenu.Length)];
                int qty = rand.Next(1, 3); // 1-2 gelas
                if (!DrinkOrders.ContainsKey(drink))
                    DrinkOrders[drink] = qty;
            }
        }

        public string GetOrderSummary()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var mie in MieOrders)
            {
                sb.AppendLine($"{mie.Value} x {mie.Key}");
            }
            return sb.ToString().TrimEnd();
        }

        public string GetDrinkSummary()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var drink in DrinkOrders)
            {
                sb.AppendLine($"{drink.Value} x {drink.Key}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}