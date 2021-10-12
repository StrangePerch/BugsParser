using System.Collections.Generic;
using System.Linq;

namespace BugsParser
{
    public class SummaryGenerator
    {
        private readonly List<string[]> _reports;
        private readonly string[] _headers;
        private readonly List<string> _teams = new();
        private readonly List<string> _priorities = new();

        public SummaryGenerator(List<string[]> reports)
        {
            _reports = reports;
            _headers = _reports.First();
            _reports.RemoveAt(0);
            GetTeamsAndReports();
            InitializeSummary();
            Generate();
        }

        public IEnumerable<string> Teams => _teams;
        public IEnumerable<string> Priorities => _priorities;
        public IEnumerable<string> Headers => _headers;
        
        public readonly Dictionary<string, Dictionary<string, int>> Summary = new();

        private void GetTeamsAndReports()
        {
            foreach (var row in _reports)
            {
                var team = row[4].Split(", ").FirstOrDefault(label => label.Contains("TEAM_"));
                if (team != null && !_teams.Contains(team))
                    _teams.Add(team);
                if (!_priorities.Contains(row[9]))
                    _priorities.Add(row[9]);
            }

            _priorities.Sort();
            _priorities.Add("Total");
            _priorities.Add("Unresolved");
            _teams.Sort();
            _teams.Add("MISC");
            _teams.Add("Total");
        }

        private void InitializeSummary()
        {
            foreach (var priority in _priorities)
            {
                Summary.Add(priority, new Dictionary<string, int>());
                foreach (var team in _teams)
                {
                    Summary[priority].Add(team, 0);
                }
            }
        }

        private void Generate()
        {
            foreach (var report in _reports)
            {
                var teamName = report[4].Split(", ")
                    .First(i => i.Contains("TEAM_")
                                || i.Contains("MISC"));
                var priority = report[9];

                Summary[priority][teamName]++;
                Summary[priority]["Total"]++;
                Summary["Total"][teamName]++;
                Summary["Total"]["Total"]++;

                if (!report[2].Contains("Closed"))
                {
                    Summary["Unresolved"][teamName]++;
                    Summary["Unresolved"]["Total"]++;
                }
            }
        }
    }
}