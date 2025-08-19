using System.Text.RegularExpressions;

namespace Scoreboard
{
    public class Scoreboard
    {
        private List<Match> _matches = new();

        public void StartMatch(string homeTeam, string awayTeam)
        {
            _matches.Add(new Match(homeTeam, awayTeam));
        }

        public bool FinishMatch(string homeTeam, string awayTeam)
        {
            var match = FindMatch(homeTeam, awayTeam);
            if (match == null)
            {
                return false;
            }

            _matches.Remove(match);
            return true;
        }

        public bool UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = FindMatch(homeTeam, awayTeam);
            if (match == null)
            {
                return false;
            }

            match.UpdateScore(homeScore, awayScore);
            return true;
        }

        public List<Match> GetSummary()
        {
            return _matches
                .OrderByDescending(m => m.TotalScore)
                .ThenByDescending(m => m.StartTime)
                .ToList();
        }

        private Match FindMatch(string homeTeam, string awayTeam)
        {
            var match = _matches.FirstOrDefault(m =>
                m.HomeTeam == homeTeam && m.AwayTeam == awayTeam);

            return match;
        }
    }
}
