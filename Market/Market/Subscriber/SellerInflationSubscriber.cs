using System;
using Market.Publisher.CentralBank;

namespace Market.Subscriber
{
    public sealed class SellerInflationSubscriber : IObserver<CentralBankData>
    {
        public CentralBankData Data { get; set; }
        private IDisposable _unSubscriber;
        private Seller _seller;

        public SellerInflationSubscriber() {
        }
        public SellerInflationSubscriber(IObservable<CentralBankData> provider) {
            _unSubscriber = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<CentralBankData> provider, Seller seller)
        {
            _unSubscriber ??= provider.Subscribe(this);
            _seller = seller;
        }

        public void Unsubscribe() {
            _unSubscriber.Dispose();
        }

        public void OnCompleted() {
        }

        public void OnError(Exception error) {
        }

        public void OnNext(CentralBankData value) {
            Data = value;
            _seller?.UpdatePricesByInflation();
        }
    }
}