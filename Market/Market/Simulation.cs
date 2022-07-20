using System;
using System.Collections.Generic;
using Market.Data;

namespace Market
{
    public class Simulation
    {
        public void Simulate(int periods)
        {
            var centralBank = new CentralBank(1.02f, 0f);
            var seller = new Seller(centralBank);
            Dictionary<Type, SellerItemsData> prices = new Dictionary<Type, SellerItemsData>();
            prices.Add(seller.Items[0].GetType(), new SellerItemsData(10f));
            prices.Add(seller.Items[1].GetType(), new SellerItemsData(10f));
            var buyer = new Buyer(centralBank, seller, prices);
            while (periods > 0)
            {
                buyer.Analyse();
                Console.WriteLine(centralBank.ToString());
                centralBank.NextPeriod();
                periods--;
            }
        }
    }
}