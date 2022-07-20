using Market.Publisher.CentralBank;
using Market.Subscriber;
using NUnit.Framework;


namespace MarketTest {
    public class SellerInflationSubscriberTest {
        private InflationProvider _provider;
        [SetUp]
        public void Setup() {
            _provider = new InflationProvider();
        }

        [Test]
        public void CheckIfTestWorks() {
            Assert.Pass();
        }

        [Test]
        public void CheckIfFirstChangeWorks() {
            SellerInflationSubscriber subscriber = new SellerInflationSubscriber(_provider);
            _provider.SetMeasurements(1.15f);
            Assert.That(subscriber.Data.InflationLevel, Is.EqualTo(1.15f).Within(0.001f));
        }

        [Test]
        public void CheckIfMultipleChangesWorks() {
            SellerInflationSubscriber subscriber = new SellerInflationSubscriber(_provider);
            _provider.SetMeasurements(1.15f);
            _provider.SetMeasurements(1.10f);
            _provider.SetMeasurements(0.90f);
            Assert.That(subscriber.Data.InflationLevel, Is.EqualTo(0.90f).Within(0.001f));
        }
    }
}