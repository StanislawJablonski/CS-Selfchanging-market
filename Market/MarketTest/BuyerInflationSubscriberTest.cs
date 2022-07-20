using Market.Publisher.CentralBank;
using Market.Subscriber;
using NUnit.Framework;


namespace MarketTest {
    public class BuyerInflationSubscriberTest {
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
        public void CheckIfChangeWorks() {
            BuyerInflationSubscriber subscriber = new BuyerInflationSubscriber(_provider);
            _provider.SetMeasurements(1.20f);
            Assert.That(subscriber.Data.InflationLevel, Is.EqualTo(1.20f).Within(0.01f));
        }

        [Test]
        public void CheckIfMultipleChangesWorks() {
            BuyerInflationSubscriber subscriber = new BuyerInflationSubscriber(_provider);
            _provider.SetMeasurements(1.20f);
            _provider.SetMeasurements(1.10f);
            _provider.SetMeasurements(0.99f);
            Assert.That(subscriber.Data.InflationLevel, Is.EqualTo(0.99f).Within(0.01f));
        }
    }
}