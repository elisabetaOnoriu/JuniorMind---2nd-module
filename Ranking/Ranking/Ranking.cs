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
            for (int i = 1; i < length; i++)
            {
                for (int j = i; j >= 0 && teams[j - 1].IsLessThan(teams[j]); j--)
                {
                    (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                }
            } 
        }
    }
}