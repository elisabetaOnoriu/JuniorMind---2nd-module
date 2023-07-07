global using Xunit;
using LinqOperations;
namespace StockFacts
{
    public class TestProgram
    {
        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan10Units()
        {
            Stock stock = new();
            Product conditioner = new("Conditioner");
            stock.AddProduct(conditioner, 20);
            stock.SellItem(conditioner, 11, e => stock.message = stock.GetMessage(e));
            Assert.Equal("There are less than 10 units of Conditioner available on stock.\n9 items are left.", stock.message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan5Units()
        {
            Product shampoo = new("Shampoo");
            Stock stock = new();
            stock.AddProduct(shampoo, 15);           
            stock.SellItem(shampoo, 11, e => stock.message = stock.GetMessage(e));        
            Assert.Equal("There are less than 5 units of Shampoo available on stock.\n4 items are left.", stock.message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan2Units()
        {
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(mango, 15);
            stock.SellItem(mango, 15, e => stock.message = stock.GetMessage(e));
            Assert.Equal("There are less than 2 units of mango available on stock.\n0 items are left.", stock.message);
        }
    }
}