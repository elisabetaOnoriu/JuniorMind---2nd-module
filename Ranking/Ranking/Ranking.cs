namespace Ranking
{
    public class Ranking
    {
        Team[] teams;

        public Ranking(Team[] teams)
        {
            this.teams = teams;
        }

        public void Add(Team team)
        {
            Array.Resize(ref teams, teams.Length + 1);
            teams[^1] = team;
            this.UpdateRanking();
        }

        public Team TeamAt(int position)
        {
            return teams[position];
        }

        public int PositionOf(Team toFind)
        {
            return Array.IndexOf(teams, toFind);
        }

        private void UpdateRanking()
        {
            int length = teams.Length;
            var key = teams[length - 1];
            var k = length - 2;
            while (k >= 0 && teams[k].IsLessThan(key))
            {
                teams[k + 1] = teams[k];
                k--;
            }
            teams[k + 1] = key;
        }
    }
}