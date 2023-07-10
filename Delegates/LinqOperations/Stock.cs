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

            notify = action;
            var previousThreshold = GetThreshold(product);                    
            products[product] -= quantity;
            var actualThreshold = GetThreshold(product);
            if (previousThreshold != actualThreshold)
            {
                NotifyLowStock(product);
            }                   
        }

        public int GetHashCode(Product product)
        {
            int keyHash = Math.Abs(product.GetHashCode());
            return keyHash >= products.Count ? keyHash % products.Count : keyHash;
        }

        private void NotifyLowStock(Product product) => notify(product, products[product]);

        private int GetThreshold(Product product)
        {
            return thresholds.FirstOrDefault(threshold => products[product] < threshold);
        }
    }
}