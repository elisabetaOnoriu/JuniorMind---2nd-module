namespace LinqOperations
{
    public class Stock
    {
        Dictionary<Product, int> products;
        readonly int[] thresholds;
        public string message;

        public Stock()
        {
            products = new();
            thresholds = new int[] { 2, 5, 10 };
        }

        public int this[Product product] { get => products[product]; set => products[product] = value; }

        public void AddProduct(Product product, int quantity)
        {
            if (products.ContainsKey(product))
            {
                products[product] += quantity;
                return;
            }

            products[product] = quantity;
        }

        public void SellItem(Product product, int quantity, Action<Product> action)
        {
            if (!products.ContainsKey(product))
            {
                throw new ArgumentException("Product is not available.");
            }

            if (products[product] == 0)
            {
                throw new ArgumentException("Product is not on stock.");
            }

            products[product] -= quantity;
            NotifyLowStock(action);
        }

        public string GetMessage(Product product)
        {
            var quantity = products[product] < thresholds[0] ? thresholds[0] :
                products[product] < thresholds[1] ? thresholds[1] :
                products[product] < thresholds[2] ? thresholds[2] : -1;
            return $"There are less than {quantity} units of {product.Name} available on stock." +
                   $"\n{products[product]} items are left.";
        }

        private void NotifyLowStock(Action<Product> action)
        {
            products.Where(a => a.Value < 10).ToList().ForEach(a => action(a.Key));
            DisplayMessage();
        }

        private void DisplayMessage() => Console.WriteLine(message);
    }
}