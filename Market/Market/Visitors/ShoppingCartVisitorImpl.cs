using Market.Data;

namespace Market.Visitors
{
    public class ShoppingCartVisitorImpl : IShoppingCartVisitor
    {
        public float Visit(Fruit fruit) => fruit.ProductionCosts * fruit.Margin;
        public float Visit(Book book) => book.ProductionCosts * book.Margin;
        
    }
}