using System.Text.RegularExpressions;

namespace Scoreboard
{
    public class Scoreboard
    {
        private List<Match> _matches = new();

        public bool StartMatch(string homeTeam, string awayTeam)
        {
            if (IsTeamPlaying(homeTeam) || IsTeamPlaying(awayTeam)) return false;

            _matches.Add(new Match(homeTeam, awayTeam));
            return true;
        }

        public bool FinishMatch(string homeTeam, string awayTeam)
        {
            var match = FindMatch(homeTeam, awayTeam);
            if (match == null) return false;

            _matches.Remove(match);
            return true;
        }

        public bool UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = FindMatch(homeTeam, awayTeam);
            if (match == null) return false;

            match.UpdateScore(homeScore, awayScore);
            return true;
        }

        //read only to prevent caller to modify list 
        public IReadOnlyList<Match> GetSummary() =>
            _matches
                .OrderByDescending(m => m.TotalScore)
                .ThenByDescending(m => m.StartTime)
                .ToList()
                .AsReadOnly();


        private Match FindMatch(string homeTeam, string awayTeam)
        {
            var match = _matches.FirstOrDefault(m =>
                m.HomeTeam == homeTeam && m.AwayTeam == awayTeam);

            return match;
        }

        private bool IsTeamPlaying(string teamName)
        {
            return _matches.Any(m => (m.HomeTeam.Equals(teamName, StringComparison.OrdinalIgnoreCase)) ||
                (m.AwayTeam.Equals(teamName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
