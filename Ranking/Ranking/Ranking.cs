namespace Ranking
{
    public class Ranking
    {
        List<Team> teams;

        public Ranking(List<Team> teams)
        {
            this.teams = teams;
        }

        public void Add(Team team)
        {
            teams.Add(team);
            var ranking = new Ranking(teams);
            ranking.UpdateRanking();
            teams = ranking.teams;
        }

        public string TeamAt(int position)
        {
            return teams[position].ToString();
        }

        public int PositionOf(Team toFind)
        {
            return teams.IndexOf(toFind);
        }

        private void UpdateRanking()
        {
            for (int j = 0; j < teams.Count - 1; j++)
            {
                for (int i = 0; i < teams.Count - 1; i++)
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