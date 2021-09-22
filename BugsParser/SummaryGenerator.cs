using System.Collections.Generic;
using System.Linq;

namespace BugsParser
{
    public class SummaryGenerator
    {
        private readonly List<string[]> _reports;
        public string[] Headers { get; }
        public List<string> Teams { get; private set; }
        public List<string> Priorities { get; private set; }
        public Dictionary<string, Dictionary<string, int>> Summary { get; private set; }
        public SummaryGenerator(List<string[]> reports)
        {
            _reports = reports;
            Headers = _reports.First();
            _reports.RemoveAt(0);
            GetTeamsAndReports();
            InitializeSummary();
            Generate();
        }
        private void GetTeamsAndReports()
        {
            Teams = new List<string>();
            Priorities = new List<string>();
            foreach (var row in _reports)
            {
                var team = row[4].Split(", ").FirstOrDefault(label => label.Contains("TEAM_"));
                if (team != null && !Teams.Contains(team))
                    Teams.Add(team);
                if(!Priorities.Contains(row[9]))
                    Priorities.Add(row[9]);
            }
            Priorities.Sort();
            Priorities.Add("Total");
            Priorities.Add("Unresolved");
            Teams.Sort();
            Teams.Add("MISC");
            Teams.Add("Total");
        }

        private void InitializeSummary()
        {
            Summary = new Dictionary<string, Dictionary<string, int>>();

            foreach (var priority in Priorities)
            {
                Summary.Add(priority, new Dictionary<string, int>());
                foreach (var team in Teams)
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