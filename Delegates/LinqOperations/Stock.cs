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
            action(product);
            DisplayMessage();
        }

        public string GetMessage(Product product)
        {
            var quantity = products[product] < thresholds[0] ? thresholds[0] :
                products[product] < thresholds[1] ? thresholds[1] :
                products[product] < thresholds[2] ? thresholds[2] : -1;
            return $"There are less than {quantity} units of {product.Name} available on stock." +
                   $"\n{products[product]} items are left.";
        }

        public bool IsLow(Product product)
        {
            return products[product] < 10;
        }

        private void DisplayMessage() => Console.WriteLine(message);
    }
}