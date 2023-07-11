namespace LinqOperations
{
    public class Product
    {
        public Product(string name, string description = "Product for sale")
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Product product && Name == product.Name && Description == product.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }
    }
}
