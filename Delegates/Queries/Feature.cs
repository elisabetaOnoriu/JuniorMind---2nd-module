namespace Queries
{
    public class Feature
    {
        public int Id { get; set; }

        public Feature(int id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Feature feature && Id == feature.Id;
        }

        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
