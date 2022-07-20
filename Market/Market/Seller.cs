using System;
using System.Collections.Generic;
using Market.Data;
using Market.Publisher.Seller;
using Market.Subscriber;
using Market.Visitors;

namespace Market
{
    public class Seller
    {
        public readonly List<CartItem> Items = new();
        private readonly CentralBank _centralBank;
        private readonly SellerInflationSubscriber _inflationSubscriber = new();
        public readonly SellerPricePublisher Provider = new();

        public Seller() {
            Provider = new SellerPricePublisher();
        }
        public Seller(CentralBank centralBank) {
            _centralBank = centralBank;
            _inflationSubscriber.Subscribe(centralBank.Provider, this);
            Items.Add(new Book("Guide", 1.2f, 12.0f, 1.0f));
            Items.Add(new Fruit("Plum", 1.15f, 5.0f, 1.0f));
            GetItems();
            Provider.SetMeasurements(Items);
        }

        public void NextSellingPeriod() {
            GetItems();
            UpdatePrices();
        }

        public void GetItems() {
            Random rnd = new Random();
            foreach (CartItem item in Items) {
                item.AmountInStock += rnd.Next() % 10 + 1;
            }
        }

        private void UpdatePrices() {
            foreach (CartItem item in Items) {
                item.ProductionCosts *= _inflationSubscriber.Data.InflationLevel;
                if (item.ItemsSold > 4)
                    item.Margin *= (1 + ((item.ItemsSold) * 0.10f));
                else
                    item.Margin *= (1 - ((item.ItemsSold) * 0.10f));
            }
            Provider.SetMeasurements(Items);
        }

        public void UpdatePricesByInflation() {
            foreach (var goods in Items) {
                goods.ProductionCosts *= _inflationSubscriber.Data.InflationLevel;
            }
            Provider.SetMeasurements(Items);
        }

        public void SellItems(Type type, float amount) {
            foreach (CartItem item in Items) {
                if (item.GetType() == type) {
                    item.AmountInStock -= amount;
                    _centralBank.PayTax(amount * item.Accept(new ShoppingCartVisitorImpl()) * 0.23f);
                }
            }
        }
    }
}