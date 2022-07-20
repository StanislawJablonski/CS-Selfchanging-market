using System;
using System.Collections.Generic;

namespace Market.Publisher.CentralBank
{
    public class InflationUnSubscriber : IDisposable
    {
        private readonly List<IObserver<CentralBankData>> _lstObservers;
        private readonly IObserver<CentralBankData> _observer;

        internal InflationUnSubscriber(List<IObserver<CentralBankData>> observersCollection,
            IObserver<CentralBankData> observer)
        {
            _lstObservers = observersCollection;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null) _lstObservers.Remove(_observer);
        }
    }
}