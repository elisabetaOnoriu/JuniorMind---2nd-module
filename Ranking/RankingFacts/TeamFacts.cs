using Xunit;

namespace Ranking.Facts
{
    public class TeamFacts
    {
        [Fact]
        public void GetRealPoints()
        {
            Team team = new Team("CFR Cluj", 25);
            Team otherTeam = new Team("FCSB", 26);
            Assert.True(team.IsLessThan(otherTeam));
        }
    }
}