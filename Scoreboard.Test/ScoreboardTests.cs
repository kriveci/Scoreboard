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
        public void FinishMatch_ExistingMatch_ShouldRemoveMatchFromScoreboard()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Slovenia", "Croatia");
            scoreboard.FinishMatch("Slovenia", "Croatia");

            var summary = scoreboard.GetSummary();

            Assert.Empty(summary);
        }

        [Fact]
        public void FinishMatch_NonExistingMatch_ShouldReturnFalse()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Slovenia", "Croatia");
            var result = scoreboard.FinishMatch("Austria", "Croatia");

            Assert.False(result);
        }

        [Fact]
        public void GetSummary_ShouldOrderByTotalScoreAndTimeStarted()
        {
            var scoreboard = new Scoreboard();

            scoreboard.StartMatch("Mexico", "Canada");
            scoreboard.UpdateScore("Mexico", "Canada", 0, 5);

            scoreboard.StartMatch("Spain", "Brazil");
            scoreboard.UpdateScore("Spain", "Brazil", 10, 2);

            scoreboard.StartMatch("Germany", "France");
            scoreboard.UpdateScore("Germany", "France", 2, 2);

            scoreboard.StartMatch("Uruguay", "Italy");
            scoreboard.UpdateScore("Uruguay", "Italy", 6, 6);

            scoreboard.StartMatch("Argentina", "Australia");
            scoreboard.UpdateScore("Argentina", "Australia", 3, 1);

            var summary = scoreboard.GetSummary();

            Assert.Equal("Uruguay", summary[0].HomeTeam);
            Assert.Equal("Spain", summary[1].HomeTeam);
            Assert.Equal("Mexico", summary[2].HomeTeam);
            Assert.Equal("Argentina", summary[3].HomeTeam);
            Assert.Equal("Germany", summary[4].HomeTeam);
        }
    }
}