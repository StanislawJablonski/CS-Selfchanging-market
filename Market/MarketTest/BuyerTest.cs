using NUnit.Framework;
using Market;
using Market.Data;
using System.Collections.Generic;
using System;

namespace MarketTest {
    public class BuyerTest {
        private CentralBank _centralBank;
        private Seller _seller;
        private Buyer _buyer;
        [SetUp]
        public void Setup() {
            _centralBank = new CentralBank(1.10f, 100f);
            _seller = new Seller(_centralBank);
            Dictionary<Type, SellerItemsData> prices = new Dictionary<Type, SellerItemsData>();
            prices.Add(_seller.Items[0].GetType(), new SellerItemsData(15f));
            prices.Add(_seller.Items[1].GetType(), new SellerItemsData(15f));
            _buyer = new Buyer(_centralBank, _seller, prices);
        }

        [Test]
        public void CheckIfTestWorks() {
            Assert.Pass();
        }

        [Test]
        public void CheckIfCanUpdateNeedForItems() {
            float need = _buyer.CheckNeedForItems(new Fruit().GetType());
            _buyer.UpdateNeedForItems();
            Assert.That(need, Is.LessThan(_buyer.CheckNeedForItems(new Fruit().GetType())));
        }

        [Test]
        public void CheckIfAnalysingWorks() {
            _centralBank.NextPeriod();
            _buyer.AnalyseForTests();
            Assert.That(_centralBank.CheckPublicIncome(), Is.GreaterThan(30));
        }

        [Test]
        public void CheckIfBuyingFromSellerWorks() {
            _centralBank.NextPeriod();
            _buyer.BuyFromSeller(0);
            Assert.That(_centralBank.CheckPublicIncome(), Is.GreaterThan(30));
        }
    }
}