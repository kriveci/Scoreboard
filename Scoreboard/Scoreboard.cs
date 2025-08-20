using System.Text.RegularExpressions;

namespace Scoreboard
{
    public class Scoreboard
    {
        private readonly List<Match> _matches = new();

        public Guid? StartMatch(string homeTeam, string awayTeam)
        {
            if (IsTeamPlaying(homeTeam) || IsTeamPlaying(awayTeam)) return null;

            var match = new Match(homeTeam, awayTeam);
            _matches.Add(match);
            return match.Id;
        }

        public bool FinishMatch(Guid matchId)
        {
            var match = FindMatch(matchId);
            if (match == null) return false;

            _matches.Remove(match);
            return true;
        }

        public bool UpdateScore(Guid matchId, int homeScore, int awayScore)
        {
            var match = FindMatch(matchId);
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


        private Match? FindMatch(Guid matchId)
        {
            var match = _matches.FirstOrDefault(m => m.Id == matchId);

            return match;
        }

        private bool IsTeamPlaying(string teamName)
        {
            return _matches.Any(m => (m.HomeTeam.Equals(teamName, StringComparison.OrdinalIgnoreCase)) ||
                (m.AwayTeam.Equals(teamName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
