using Market.Visitors;

namespace Market.Data
{
    public abstract class CartItem
    {
        public float AmountInStock;
        public float ItemsSold;
        public abstract float Margin { get; set; }
        public abstract float ProductionCosts { get; set; }
        public abstract float Accept(IShoppingCartVisitor visitor);
    }
}