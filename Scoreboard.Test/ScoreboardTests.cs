using System.Text.RegularExpressions;

namespace Scoreboard.Test
{
    public class ScoreboardTests
    {
        [Fact]
        public void StartMatch_ShouldAddMatchWithInitialScore()
        {
            var scoreBoard = new Scoreboard();

            var matchId = scoreBoard.StartMatch("Slovenia", "Croatia");

            var summary = scoreBoard.GetSummary();

            Assert.Single(summary);
            Assert.Equal(matchId, summary[0].Id);
            Assert.Equal(0, summary[0].HomeScore);
            Assert.Equal(0, summary[0].AwayScore);
            Assert.Equal("Slovenia", summary[0].HomeTeam);
            Assert.Equal("Croatia", summary[0].AwayTeam);
        }

        [Fact]
        public void StartMatch_TeamNameEmpty_ShouldThrowArgumentExcpetion()
        {
            var scoreBoard = new Scoreboard();

            Assert.Throws<ArgumentException>(() => scoreBoard.StartMatch("", "Croatia"));
        }

        [Fact]
        public void StartMatch_SameHomeAndAwayTeam_ShouldThrowArgumentExcpetion()
        {
            var scoreBoard = new Scoreboard();

            Assert.Throws<ArgumentException>(() => scoreBoard.StartMatch("Croatia", "Croatia"));
        }

        [Fact]
        public void StartMatch_TeamAlreadyPlaying_ShouldNotAddMatch()
        {
            var scoreBoard = new Scoreboard();

            scoreBoard.StartMatch("Slovenia", "Croatia");
            scoreBoard.StartMatch("Slovenia", "Austria");

            var summary = scoreBoard.GetSummary();

            //summary should still contain single element
            Assert.Single(summary);
        }

        [Fact]
        public void StartMatch_TeamAlreadyPlaying_ShouldreturnNull()
        {
            var scoreBoard = new Scoreboard();

            scoreBoard.StartMatch("Slovenia", "Croatia");
            var matchId = scoreBoard.StartMatch("Slovenia", "Austria");

            var summary = scoreBoard.GetSummary();

            //summary should be null
            Assert.Null(matchId);
        }

        [Fact]
        public void UpdateScore_ExistingMatch_ShouldUpdateMatchScore()
        {
            var scoreboard = new Scoreboard();

            var matchId = scoreboard.StartMatch("Slovenia", "Croatia");
            if (matchId.HasValue) scoreboard.UpdateScore(matchId.Value, 1, 0);

            var summary = scoreboard.GetSummary();

            Assert.Single(summary);
            Assert.Equal(1, summary[0].HomeScore);
            Assert.Equal(0, summary[0].AwayScore);
        }

        [Fact]
        public void UpdateScore_NonExistingMatch_ShouldNotUpdateMatchScore()
        {
            var scoreboard = new Scoreboard();

            var matchId = scoreboard.StartMatch("Slovenia", "Croatia");
            scoreboard.UpdateScore(Guid.NewGuid(), 1, 0);

            var summary = scoreboard.GetSummary();

            Assert.Single(summary);
            Assert.Equal(0, summary[0].HomeScore);
            Assert.Equal(0, summary[0].AwayScore);
        }

        [Fact]
        public void UpdateScore_NonExistingMatch_ShouldReturnFalse()
        {
            var scoreboard = new Scoreboard();

            var matchId = scoreboard.StartMatch("Slovenia", "Croatia");
            bool result = scoreboard.UpdateScore(Guid.NewGuid(), 1, 0);
                        
            Assert.False(result);
        }

        [Fact]
        public void FinishMatch_ExistingMatch_ShouldRemoveMatchFromScoreboard()
        {
            var scoreboard = new Scoreboard();

            var matchId = scoreboard.StartMatch("Slovenia", "Croatia");
            if (matchId.HasValue) scoreboard.FinishMatch(matchId.Value);

            var summary = scoreboard.GetSummary();

            Assert.Empty(summary);
        }

        [Fact]
        public void FinishMatch_NonExistingMatch_ShouldReturnFalse()
        {
            var scoreboard = new Scoreboard();

            var matchId = scoreboard.StartMatch("Slovenia", "Croatia");
            var result = scoreboard.FinishMatch(Guid.NewGuid());

            Assert.False(result);
        }

        [Fact]
        public void GetSummary_ShouldOrderByTotalScoreAndTimeStarted()
        {
            var scoreboard = new Scoreboard();

            var match1 = scoreboard.StartMatch("Mexico", "Canada");
            if (match1.HasValue) scoreboard.UpdateScore(match1.Value, 0, 5);

            var match2 = scoreboard.StartMatch("Spain", "Brazil");
            if (match2.HasValue) scoreboard.UpdateScore(match2.Value, 10, 2);

            var match3 = scoreboard.StartMatch("Germany", "France");
            if (match3.HasValue) scoreboard.UpdateScore(match3.Value, 2, 2);

            var match4 = scoreboard.StartMatch("Uruguay", "Italy");
            if (match4.HasValue) scoreboard.UpdateScore(match4.Value, 6, 6);

            var match5 = scoreboard.StartMatch("Argentina", "Australia");
            if (match5.HasValue) scoreboard.UpdateScore(match5.Value, 3, 1);

            var summary = scoreboard.GetSummary();

            Assert.Equal("Uruguay", summary[0].HomeTeam);
            Assert.Equal("Spain", summary[1].HomeTeam);
            Assert.Equal("Mexico", summary[2].HomeTeam);
            Assert.Equal("Argentina", summary[3].HomeTeam);
            Assert.Equal("Germany", summary[4].HomeTeam);
        }
    }
}