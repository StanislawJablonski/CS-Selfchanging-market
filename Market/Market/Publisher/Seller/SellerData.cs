using System.Collections.Generic;
using Market.Data;

namespace Market.Publisher.Seller
{
    public class SellerData
    {
        public List<CartItem> Products;
        public SellerData(List<CartItem> products) {
            Products = products;
        }
    }
}