using Market;
using NUnit.Framework;


namespace MarketTest {
    public class SellerTest {
        private CentralBank _centralBank;
        private Seller _seller;
        [SetUp]
        public void Setup() {
            _centralBank = new CentralBank(1.05f, 100f);
            _seller = new Seller(_centralBank);
        }

        [Test]
        public void CheckIfTestWorks() {
            Assert.Pass();
        }

        [Test]
        public void CheckIfCanGetItems() {
            float stock = _seller.Items[0].AmountInStock;
            _seller.GetItems();
            Assert.That(stock, Is.LessThan(_seller.Items[0].AmountInStock));
        }

        [Test]
        public void CheckIfCanUpdatePrices() {
            _centralBank.NextPeriod();
            _seller.NextSellingPeriod();
            Assert.That(_seller.Items[0].ProductionCosts, Is.EqualTo(17.28f).Within(0.01f));
        }

        [Test]
        public void CheckIfCanBuyItems() {
            float stock = _seller.Items[0].AmountInStock;
            _seller.SellItems(_seller.Items[0].GetType(), 1.0f);
            Assert.That(_seller.Items[0].AmountInStock, Is.EqualTo(stock-1.0f).Within(0.01f));
        }
    }
}