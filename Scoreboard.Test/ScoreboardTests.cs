namespace Scoreboard.Test
{
    public class ScoreboardTests
    {
        [Fact]
        public void StartMatch_AddsMatchWithInitialScore()
        {
            var scoreBoard = new Scoreboard();

            scoreBoard.StartMatch("Slovenia", "Croatia");

            var summary = scoreBoard.GetSummary();

            Assert.Single(summary);
            Assert.Equal(0, summary[0].HomeScore);
            Assert.Equal(0, summary[0].AwayScore);
            Assert.Equal("Slovenia", summary[0].HomeTeam);
            Assert.Equal("Croatia", summary[0].AwayTeam);
        }
    }
}