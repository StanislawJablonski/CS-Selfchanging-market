using Market.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using Market.Data;

namespace Market {
    public class Buyer {
        private  BuyerInflationSubscriber _inflationSubscriber = new ();
        private readonly List<BuyerItemsSubscriber> _itemsSubscriber = new ();
        private readonly Dictionary<int, Seller> _sellers = new ();
        private readonly Dictionary<Type, SellerItemsData> _itemsInfo = new ();
        private readonly CentralBank _centralBank;
        private bool DoIBuy { get; set; }

        public Buyer() { }
        public Buyer(CentralBank centralBank, Seller seller, Dictionary<Type, SellerItemsData> itemsPrices) {
            _centralBank = centralBank;
            _inflationSubscriber.Subscribe(centralBank.Provider, this);
            _sellers.Add(0, seller);
            _itemsSubscriber.Add(new BuyerItemsSubscriber());
            _itemsSubscriber[0].Subscribe(_sellers[0].Provider, this, 0);
            _itemsInfo = itemsPrices;
            Analyse();
        }

        public Buyer(CentralBank centralBank, List<Seller> seller, Dictionary<Type, SellerItemsData> itemsPrices) {
            _itemsInfo = itemsPrices;
            _inflationSubscriber.Subscribe(centralBank.Provider, this);
            for (int i = 0; i < seller.Count(); i++) {
                _sellers.Add(i, seller[i]);
            }
            for (int i = 0; i < _sellers.Count(); i++) {
                _itemsSubscriber.Add(new BuyerItemsSubscriber());
                _itemsSubscriber[i].Subscribe(_sellers[i].Provider, this, i);
            }
            Analyse();
        }

        public void NextBuyingPeriod() {
            UpdateNeedForItems();
            Analyse();
        }

        public void UpdateNeedForItems() {
            Random rnd = new Random();
            foreach (var items in _itemsInfo.Values) {
                items.AmountNeeded += (rnd.Next() % 10) + 1;
            }
        }
        


        public void Analyse() {
            foreach (var seller in _sellers.Values) {
                foreach (var item in seller.Items) {
                    if (_itemsInfo.Keys.Contains(item.GetType())) {
                        var random = new Random();
                        var limit = (int) (100 * _centralBank.GetInflation());
                        DoIBuy = random.Next(0, limit) > 50;
                        if (DoIBuy)
                        {
                            seller.SellItems(item.GetType(), _itemsInfo[item.GetType()].AmountNeeded);
                        }
                    }
                }
            }
        }
        
        public void AnalyseForTests() {
            foreach (var seller in _sellers.Values)
            {
                foreach (var item in seller.Items.Where(item => _itemsInfo.Keys.Contains(item.GetType())))
                {
                    seller.SellItems(item.GetType(), _itemsInfo[item.GetType()].AmountNeeded);
                }
            }
        }

        public void BuyFromSeller(int sellerId)
        {
            Seller seller = _sellers[sellerId];
            foreach (var item in seller.Items.Where(item => _itemsInfo.Keys.Contains(item.GetType())))
            {
                seller.SellItems(item.GetType(), (_itemsInfo[item.GetType()].AmountNeeded));
            }
        }

        public float CheckNeedForItems(Type type) {
            return _itemsInfo[type].AmountNeeded;
        }
    }
}