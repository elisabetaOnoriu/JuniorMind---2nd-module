namespace Queries
{
    public class ProductFeature
    {
        public string Name { get; set; }
        public ICollection<Feature> Features { get; set; } = new List<Feature>();

        public ProductFeature(string name, params int[] featureId)
        {
            Name = name;
            foreach (int id in featureId)
            {
                Features.Add(new Feature(id));
            }
        }
    }
}
