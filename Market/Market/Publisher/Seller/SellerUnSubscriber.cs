using System;
using System.Collections.Generic;

namespace Market.Publisher.Seller
{
    public class SellerUnSubscriber : IDisposable
    {
        private List<IObserver<SellerData>> _lstObservers;
        private IObserver<SellerData> _observer;

        internal SellerUnSubscriber(List<IObserver<SellerData>> observers, IObserver<SellerData> observer)
        {
            _lstObservers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null) _lstObservers.Remove(_observer);
        }
    }
}