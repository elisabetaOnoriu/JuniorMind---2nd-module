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
            Action<Product> action = product => stock.message = stock.IsLow(product) ?
            stock.GetMessage(product) : string.Empty;
            stock.SellItem(conditioner, 11, action);
            Assert.Equal(stock.GetMessage(conditioner), stock.message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan5Units()
        {
            Product shampoo = new("Shampoo");
            Stock stock = new();
            stock.AddProduct(shampoo, 15);
            Action<Product> action = product => stock.message = stock.IsLow(product) ? 
            stock.GetMessage(product) : string.Empty;
            stock.SellItem(shampoo, 11, action);        
            Assert.Equal(stock.GetMessage(shampoo), stock.message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan2Units()
        {
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(mango, 15);
            Action<Product> action = product => stock.message = stock.IsLow(product) ?
            stock.GetMessage(product) : string.Empty;
            stock.SellItem(mango, 15, action);
            Assert.Equal(stock.GetMessage(mango), stock.message);
        }
    }
}