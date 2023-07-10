namespace LinqOperations
{
    public class Stock
    {
        Dictionary<Product, int> products;
        public readonly int[] thresholds;
        Action<Product, int> notify;

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

        public void SellItem(Product product, int quantity, Action<Product, int> action)
        {
            if (!products.ContainsKey(product))
            {
                throw new ArgumentException("Product is not available.");
            }

            if (products[product] == 0)
            {
                throw new ArgumentException("Product is not on stock.");
            }

            var threshold = GetThreshold(product);         
            notify = action;
            products[product] -= quantity;
            if (threshold > GetThreshold(product))
            {
                NotifyLowStock(product);
            }                   
        }

        private void NotifyLowStock(Product product) => notify(product, products[product]);

        private int GetThreshold(Product product)
        {
            try
            {
                return thresholds.Where(threshold => products[product] < threshold).First();
            }
            catch
            {
                return 20000;
            }
        }
    }
}