using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard
{
    public class Match
    {
        public Guid Id { get; }
        public string HomeTeam { get; }
        public string AwayTeam { get; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }
        public DateTime StartTime { get; }
        public int TotalScore => HomeScore + AwayScore;

        public Match(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
                throw new ArgumentException("Team name can't be null or empty.");

            if (homeTeam.Equals(awayTeam, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Home and away teams must be different.");

            Id = Guid.NewGuid();
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            StartTime = DateTime.UtcNow;
        }

        public void UpdateScore(int homeScore, int awayScore)
        {
            if (homeScore < 0 || awayScore < 0)
                throw new ArgumentException("Score can't be negative.");
            HomeScore = homeScore;
            AwayScore = awayScore;
        }
    }
}
