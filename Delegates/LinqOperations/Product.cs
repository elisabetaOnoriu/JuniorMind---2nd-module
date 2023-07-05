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
    }
}
