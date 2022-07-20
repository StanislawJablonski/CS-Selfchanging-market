using System;
using System.Collections.Generic;
using Market.Data;

namespace Market.Publisher.Seller
{
    public class SellerPricePublisher : IObservable<SellerData>
    {
        private List<IObserver<SellerData>> _observers;
        
        public SellerPricePublisher() {
            _observers = new List<IObserver<SellerData>>();
        }

        public IDisposable Subscribe(IObserver<SellerData> observer) {
            if (!_observers.Contains(observer)) {
                _observers.Add(observer);
            }
            return new SellerUnSubscriber(_observers, observer);
        }

        private void MeasurementsChanged(List<CartItem> items) {
            foreach (var obs in _observers) {
                obs.OnNext(new SellerData(items));
            }
        }

        public void SetMeasurements(List<CartItem> items) {
            MeasurementsChanged(items);
        }
    }
}