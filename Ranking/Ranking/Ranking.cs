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
            for (int j = 0; j < teams.Length - 1; j++)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    if (teams[i].IsLessThan(teams[i + 1]))
                    {
                        (teams[i], teams[i + 1]) = (teams[i + 1], teams[i]);
                    }
                }
            }
        }
    }
}