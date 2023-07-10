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
            Action<Product, int> action = (product, items) =>
            message = $"There are {items} items left of {product.Name} product.";
            stock.SellItem(conditioner, 11, action);
            Assert.Equal("There are 9 items left of Conditioner product.", message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan5Units()
        {
            Product shampoo = new("Shampoo");
            Stock stock = new();
            stock.AddProduct(shampoo, 15);
            string message = string.Empty;
            Action<Product, int> action = (product, items) =>
            message = $"There are {items} items left of {product.Name} product.";
            stock.SellItem(shampoo, 11, action);
            Assert.Equal("There are 4 items left of Shampoo product.", message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_LessThan2Units()
        {
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(mango, 15);
            string message = string.Empty;
            Action<Product, int> action = (product, items) =>
            message = $"There are {items} items left of {product.Name} product.";
            stock.SellItem(mango, 15, action);
            Assert.Equal("There are 0 items left of mango product.", message);
        }

        [Fact]
        public void ActionTakesPlaceAtTheRightTime_DisplaysTheRightMessage_ThresholdIsNotPassed()
        {
            Product mango = new("mango");
            Stock stock = new();
            stock.AddProduct(mango, 4);
            string message = string.Empty;
            Action<Product, int> action = (product, items) =>
            message = $"There are {items} items left of {product.Name} product.";
            stock.SellItem(mango, 2, action);
            Assert.Equal(string.Empty, message);
        }
    }
}