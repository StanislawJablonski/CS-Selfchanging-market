using NUnit.Framework;
using Market;
using Market.Subscriber;

namespace MarketTest {
    public class CentralBankTest {
        private CentralBank _centralBank;
        [SetUp]
        public void Setup() {
            _centralBank = new CentralBank(1.02f, 500f);
        }

        [Test]
        public void CheckIfTestWorks() {
            Assert.Pass();
        }

        [Test]
        public void CheckIfCanCreate() {
            Assert.That(_centralBank, Is.Not.Null);
        }
        
        [Test]
        public void CheckIfCanPayTax() {
            _centralBank.PayTax(10f);
            Assert.That(_centralBank.CheckPublicIncome(), Is.EqualTo(10f).Within(0.01f));
        }

        [Test]
        public void CheckIfCalculatingInflationWorks() {
            SellerInflationSubscriber provider = new SellerInflationSubscriber(_centralBank.Provider);
            _centralBank.NextPeriod();
            float inflation = provider.Data.InflationLevel;
            Assert.That(inflation, Is.EqualTo(1.20f).Within(0.001f));
        }

        [Test]
        public void CheckIfNextPeriodWorks() {
            _centralBank.NextPeriod();
            Assert.That(_centralBank.CheckPublicIncome, Is.EqualTo(0));
        }
    }
}