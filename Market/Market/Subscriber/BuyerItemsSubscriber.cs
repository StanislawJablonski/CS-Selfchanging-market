using System;
using Market.Publisher.Seller;

namespace Market.Subscriber
{
    public class BuyerItemsSubscriber : IObserver<SellerData>
    {
        public SellerData Data { get; set; }
        private IDisposable _unsubscriber;
        private Buyer _buyer;
        private int _sellerId;

        public BuyerItemsSubscriber()
        {
        }

        public BuyerItemsSubscriber(IObservable<SellerData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<SellerData> provider, Buyer buyer, int id)
        {
            if (_unsubscriber == null) _unsubscriber = provider.Subscribe(this);
            _buyer = buyer;
            _sellerId = id;
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(SellerData value)
        {
            Data = value;
            if (_buyer != null)
                _buyer.BuyFromSeller(_sellerId);
        }
    }
}