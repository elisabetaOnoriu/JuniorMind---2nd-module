global using Xunit;
using LinqOperations;
namespace StockFacts
{
    public class TestProgram
    {
        [Fact]
        public void ActionTakesPlaceAtTheRightTime()
        {
            Stock stock = new();
            Product conditioner = new("Conditioner");
            stock.AddProduct(conditioner, 20);
            Assert.True(stock.action == null);
            stock.SellItem(conditioner, 11);
            Assert.True(stock.action != null);
        }

        [Fact]
        public void NotificationsAreShowingTheRightMessage()
        {
            Product shampoo = new("Shampoo");
            Product conditioner = new("Conditioner");
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(shampoo, 15);
            stock.AddProduct(conditioner, 20);
            stock.AddProduct(mango, 15);           
            stock.SellItem(shampoo, 6);
            stock.SellItem(conditioner, 20);
            stock.SellItem(mango, 11);          
            Assert.Equal("There are less than 5 units of mango available on stock.\n4 items are left.", stock.GetMessage(mango));
            Assert.Equal("There are less than 10 units of Shampoo available on stock.\n9 items are left.", stock.GetMessage(shampoo));
            Assert.Equal("There are less than 2 units of Conditioner available on stock.\n0 items are left.", stock.GetMessage(conditioner));
        }
    }
}