using Xunit;

namespace Ranking.Facts
{
    public class RankingFacts
    {
        [Fact]
        public void RankingIsUpdatingWhenTeamIsAdded()
        {
            List<Team> teams = new List<Team>();

            var dinamo = new Team("Dinamo", 20);
            teams.Add(new Team("FCSB", 28));
            teams.Add(new Team("Viitorul", 21));
            teams.Add(dinamo);

            var ranking = new Ranking(teams);
            ranking.Add(new Team("CFR Cluj", 26));

            Assert.Equal(new Team("CFR Cluj", 26).ToString(), ranking.TeamAt(1).ToString());

            Assert.Equal(3, ranking.PositionOf(dinamo));
        }
    }
}