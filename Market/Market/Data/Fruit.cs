using System;
using Market.Visitors;

namespace Market.Data
{
    public class Fruit : CartItem
    {
        public String Name { get; set; }

        public Fruit() { }

        public Fruit(string name, float margin, float productionCosts, float amountInStock) {
            Name = name;
            Margin = margin;
            ProductionCosts = productionCosts;
            AmountInStock = amountInStock;
        }

        public sealed override float Margin { get; set; }
        public sealed override float ProductionCosts { get; set; }
        public override float Accept(IShoppingCartVisitor visitor) => visitor.Visit(this);
    }
}