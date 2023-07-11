namespace LinqOperations
{
    public class Stock
    {
        Dictionary<Product, int> products;
        public readonly int[] thresholds;
        Action<Product, int> notify;

        public Stock(Action<Product, int> notify)
        {
            products = new();
            this.notify = notify;
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

        public void SellItem(Product product, int quantity)
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
            if (PassedThreshold(product, quantity))
            {
                NotifyLowStock(product); 
            }                             
        }

        private void NotifyLowStock(Product product) => notify(product, products[product]);

        private bool PassedThreshold(Product product, int quantitySold)
        {
            var quantity = products[product];
            return GetThreshold(quantity) != GetThreshold(quantity + quantitySold);
        }

        private int GetThreshold(int quantity) => thresholds.FirstOrDefault(threshold => quantity < threshold);
    }
}