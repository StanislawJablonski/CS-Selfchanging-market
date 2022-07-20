using System;
using System.Collections.Generic;

namespace Market.Publisher.CentralBank
{
    public class InflationProvider : IObservable<CentralBankData>
    {
        private readonly List<IObserver<CentralBankData>> _observers;

        public InflationProvider() {
            _observers = new List<IObserver<CentralBankData>>();
        }

        public IDisposable Subscribe(IObserver<CentralBankData> observer) {
            if (!_observers.Contains(observer)) {
                _observers.Add(observer);
            }
            return new InflationUnSubscriber(_observers, observer);
        }

        private void MeasurementsChanged(float inflationLevel) {
            foreach (var obs in _observers) {
                obs.OnNext(new CentralBankData(inflationLevel));
            }
        }

        public void SetMeasurements(float inflationLevel) {
            MeasurementsChanged(inflationLevel);
        }
    }
}