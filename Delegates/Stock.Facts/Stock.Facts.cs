global using Xunit;
using LinqOperations;
namespace StockFacts
{
    public class TestProgram
    {  
        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan10Units()
        {
            Product conditioner = new("Conditioner");
            string message = string.Empty;
            Stock stock = new();
            stock.Register((product, items) =>
            message = $"There are {items} items left of {product.Name} product.");
            stock.AddProduct(conditioner, 20);            
            stock.SellItem(conditioner, 11);
            Assert.Equal("There are 9 items left of Conditioner product.", message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan5Units()
        {
            Product shampoo = new("Shampoo");
            string message = string.Empty;
            Stock stock = new();
            stock.Register((product, items) =>
            message = $"There are {items} items left of {product.Name} product.");
            stock.AddProduct(shampoo, 15);            
            stock.SellItem(shampoo, 11);
            Assert.Equal("There are 4 items left of Shampoo product.", message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan2Units()
        {
            Product mango = new("mango");
            string message = string.Empty;
            Stock stock = new();
            stock.Register((product, items) =>
            message = $"There are {items} items left of {product.Name} product.");
            stock.AddProduct(mango, 15);
            stock.SellItem(mango, 15);          
            Assert.Equal("There are 0 items left of mango product.", message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_ThresholdIsNotPassed()
        {
            Product mango = new("mango");
            string message = string.Empty;
            Stock stock = new();
            stock.Register((product, items) =>
            message = $"There are {items} items left of {product.Name} product.");
            stock.AddProduct(mango, 4);          
            stock.SellItem(mango, 2);
            Assert.Equal(string.Empty, message);
        }
    }
}