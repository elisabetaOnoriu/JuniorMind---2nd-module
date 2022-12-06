using Xunit;

namespace Ranking.Facts
{
    public class RankingFacts
    {
        [Fact]
        public void RankingIsUpdatingWhenTeamIsAdded()
        {
            var dinamo = new Team("Dinamo", 20);
            Team[] teams =
        {
            new Team("FCSB", 28),
            new Team("Viitorul", 21),
            dinamo
        };

            var ranking = new Ranking(teams);
            ranking.Add(new Team("CFR Cluj", 26));

            Assert.Equal(new Team("CFR Cluj", 26).ToString(), ranking.TeamAt(1).ToString());

            Assert.Equal(3, ranking.PositionOf(dinamo));
        }
    }
}