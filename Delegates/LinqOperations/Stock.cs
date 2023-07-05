namespace LinqOperations
{
    public class Stock
    {
        Dictionary<Product, int> products;
        Action<Product> action;

        public Stock()
        {
            products = new();
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

        private void NotifyLowStock()
        {
            products.Where(a => a.Value < 10).ToList().ForEach(a => action(a.Key));
        }

        private void DisplayMessage(Product product)
        {
            var quantity = products[product] < 2 ? 2 : products[product] < 5 ? 5 : products[product] < 10 ? 10 : -1;
            if (products[product] > -1)
            {
                Console.WriteLine($"There are less than {quantity} units of {product.Name} available on stock.\n{products[product]} items are left.");
            }
        }
    }
}