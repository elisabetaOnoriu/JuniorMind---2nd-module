namespace Queries
{
    public class TestResults
    {
        public string Id { get; set; }
        public string FamilyId { get; set; }
        public int Score { get; set; }

        public TestResults(int id, string familyId, int score)
        {
            Id = Id;
            FamilyId = familyId;
            Score = score;
        }
    }
}
