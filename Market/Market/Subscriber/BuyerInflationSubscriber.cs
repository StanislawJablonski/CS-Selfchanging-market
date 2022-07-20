using System;
using Market.Publisher.CentralBank;

namespace Market.Subscriber
{
    public class BuyerInflationSubscriber : IObserver<CentralBankData>
    {
        public CentralBankData Data { get; set; }
        private IDisposable _unSubscriber;
        private Buyer _buyer;

        public BuyerInflationSubscriber()
        {
        }

        public BuyerInflationSubscriber(IObservable<CentralBankData> provider)
        {
            _unSubscriber = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<CentralBankData> provider, Buyer buyer)
        {
            _unSubscriber ??= provider.Subscribe(this);
            _buyer = buyer;
        }

        public void Unsubscribe()
        {
            _unSubscriber.Dispose();
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public virtual void OnNext(CentralBankData value)
        {
            Data = value;
            if (_buyer != null)
                _buyer.Analyse();
        }
    }
}