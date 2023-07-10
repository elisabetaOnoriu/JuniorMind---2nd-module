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
            string message = string.Empty;
            Action<Product> action = product => message += stock.GetMessage(product);
            stock.SellItem(conditioner, 11, action);
            Assert.Equal(stock.GetMessage(conditioner), message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan5Units()
        {
            Product shampoo = new("Shampoo");
            Stock stock = new();
            stock.AddProduct(shampoo, 15);
            string message = string.Empty;
            Action<Product> action = product => message += stock.GetMessage(product);
            stock.SellItem(shampoo, 11, action);        
            Assert.Equal(stock.GetMessage(shampoo), message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan2Units()
        {
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(mango, 15);
            string message = string.Empty;
            Action<Product> action = product => message += stock.GetMessage(product);
            stock.SellItem(mango, 15, action);
            Assert.Equal(message, stock.GetMessage(mango));
        }
    }
}