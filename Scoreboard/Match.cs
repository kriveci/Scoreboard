using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard
{
    public class Match
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public DateTime StartTime { get; set; }
        public int TotalScore => HomeScore + AwayScore;

        public Match(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
                throw new ArgumentException("Team name can't be null or empty.");

            if (homeTeam.Equals(awayTeam, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Home and away teams must be different.");

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
