namespace LinqOperations
{
    public class Stock
    {
        Dictionary<Product, int> products;
        public readonly int[] thresholds;

        public Stock()
        {
            products = new();
            thresholds = new int[] { 2, 5, 10 };
            Array.Sort(thresholds);
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
            if (StockIsLow())
            {
                NotifyLowStock(action);
            }          
        }

        public string GetMessage(Product product)
        {
            var quantity = thresholds.Where(threshold => products[product] < threshold).First();
            return $"There are less than {quantity} units of {product.Name} available on stock." +
                   $"\n{products[product]} items are left.";
        }

        private void NotifyLowStock(Action<Product> action)
        {
            products.Where(a => a.Value < thresholds[^1]).ToList().ForEach(a =>
            { 
                action(a.Key); 
                DisplayMessage(a.Key); 
            });
        }

        private bool StockIsLow() => products.Any(product => this[product.Key] < thresholds[^1]);

        private void DisplayMessage(Product product) => Console.WriteLine(GetMessage(product));
    }
}