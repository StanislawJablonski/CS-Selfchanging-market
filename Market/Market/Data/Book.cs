using System;
using Market.Visitors;

namespace Market.Data
{
    public class Book : CartItem
    {
        public String Title { get; set; }

        public Book(string title, float margin, float productionCosts, float amountInStock) {
            Title = title;
            Margin = margin;
            ProductionCosts = productionCosts;
            AmountInStock = amountInStock;
        }

        public sealed override float Margin { get; set; }
        public sealed override float ProductionCosts { get; set; }
        public override float Accept(IShoppingCartVisitor visitor) => visitor.Visit(this);
    }
}