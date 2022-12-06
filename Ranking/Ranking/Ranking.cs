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
            bool swapped;

            for (int j = 0; j < teams.Length - 1; j++)
            {
                swapped = false;
                for (int i = teams.Length - 1; i > 0; i--)
                {
                    if (teams[i - 1].IsLessThan(teams[i]))
                    {
                        (teams[i - 1], teams[i]) = (teams[i], teams[i - 1]);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }
    }
}