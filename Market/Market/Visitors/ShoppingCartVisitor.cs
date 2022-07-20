using Market.Data;

namespace Market.Visitors
{
    public interface IShoppingCartVisitor
    {
        float Visit(Book book);
        float Visit(Fruit fruit);
    }
}