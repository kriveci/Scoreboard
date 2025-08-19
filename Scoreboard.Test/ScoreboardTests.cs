namespace Scoreboard.Test
{
    public class ScoreboardTests
    {
        [Fact]
        public void StartMatch_ShouldAddMatchWithInitialScore()
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

        [Fact]
        public void UpdateScore_ExistingMatch_ShouldUpdateMatchScore()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Slovenia", "Croatia");
            scoreboard.UpdateScore("Slovenia", "Croatia", 1, 0);

            var summary = scoreboard.GetSummary();

            Assert.Single(summary);
            Assert.Equal(1, summary[0].HomeScore);
            Assert.Equal(0, summary[0].AwayScore);
        }

        [Fact]
        public void UpdateScore_NonExistingMatch_ShouldNotUpdateMatchScore()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Slovenia", "Croatia");
            scoreboard.UpdateScore("Italy", "Austria", 1, 0);

            var summary = scoreboard.GetSummary();

            Assert.Single(summary);
            Assert.Equal(0, summary[0].HomeScore);
            Assert.Equal(0, summary[0].AwayScore);
        }

        [Fact]
        public void UpdateScore_NonExistingMatch_ShouldReturnFalse()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Slovenia", "Croatia");
            bool result = scoreboard.UpdateScore("Italy", "Austria", 1, 0);
                        
            Assert.False(result);
        }

        [Fact]
        public void FinishMAtch_ExistingMatch_ShouldRemoveMatchFromScoreboard()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Slovenia", "Croatia");
            scoreboard.FinishMatch("Slovenia", "Croatia");

            var summary = scoreboard.GetSummary();

            Assert.Empty(summary);
        }
    }
}