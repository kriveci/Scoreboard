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

        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = FindMatch(homeTeam, awayTeam);
            match.UpdateScore(homeScore, awayScore);
        }

        public List<Match> GetSummary()
        {
            return _matches;
        }

        private Match FindMatch(string homeTeam, string awayTeam)
        {
            var match = _matches.FirstOrDefault(m =>
                m.HomeTeam == homeTeam && m.AwayTeam == awayTeam);

            return match;
        }
    }
}
