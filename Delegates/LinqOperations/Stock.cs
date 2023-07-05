namespace LinqOperations
{
    public class Stock
    {
        Dictionary<Product, int> products;
        public Action<Product> action;
        int[] thresholds;

        public Stock()
        {
            products = new();
            thresholds = new int[] { 10, 5, 2 };
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

            action = DisplayMessage;
            products[product] -= quantity;
            NotifyLowStock();
        }

        public string GetMessage(Product product)
        {
            int quantity = products[product];
            int threshold = -1;
            for (int i = 0; i < thresholds.Length && quantity < thresholds[i]; i++)
            {                
                threshold = thresholds[i];
            }

            return $"There are less than {threshold} units of {product.Name} available on stock.\n{quantity} items are left.";
        }

        private void NotifyLowStock()
        {
            products.Where(a => a.Value < 10).ToList().ForEach(a => action(a.Key));
        }

        private void DisplayMessage(Product product) => Console.WriteLine(GetMessage(product));
    }
}