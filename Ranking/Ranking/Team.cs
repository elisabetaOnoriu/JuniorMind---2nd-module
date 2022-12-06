namespace Ranking
{
    public class Team
    {
        readonly string name;
        readonly int points;

        public Team(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public bool IsLessThan(Team that)
        {
            return this.points < that.points;
        }

        public override string ToString()
        {
            return $"{this.name} + {this.points}";
        }
    }
}