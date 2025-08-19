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

        public List<Match> GetSummary()
        {
            return _matches;
        }
    }
}
