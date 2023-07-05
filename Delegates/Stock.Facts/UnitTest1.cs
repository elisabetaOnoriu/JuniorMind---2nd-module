using LinqOperations;
namespace StockFacts
{
    public class TestProgram
    {
        [Fact]
        public void Test1()
        {
            Product shampoo = new("Shampoo");
            Product conditioner = new("Conditioner");
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(shampoo, 20);
            stock.AddProduct(conditioner, 20);
            stock.AddProduct(mango, 20);
            Assert.Equal(20, stock[mango]);
        }
    }
}