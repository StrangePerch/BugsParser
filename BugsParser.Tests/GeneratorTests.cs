using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BugsParser.Tests
{
    public class GeneratorTests
    {
        [Fact]
        public void GeneratorGetHeaders_ShouldWorkOnTestData()
        {
            var expected = new []{"Key","Summary","Status","Assignee","Labels","Fix Version/s","Reporter","Issue Type","? Original Estimate","Priority","Sprint","Due Date","Created","QA Due Date"};
            var generator = new SummaryGenerator(new CsvReader("bugs-2002.csv").Read());
            var result = generator.Headers;
            Assert.Equal(expected, result);
        }
        [Fact]
        public void GeneratorGetTeams_ShouldWorkOnTestData()
        {
            var expected = new List<string>{"TEAM_BEAUJOLAIS","TEAM_BORDEAUX","TEAM_BURGUNDY","TEAM_LOIRE","TEAM_PROVENCE","TEAM_RHONE","MISC","Total"};
            var generator = new SummaryGenerator(new CsvReader("bugs-2002.csv").Read());
            var result = generator.Teams;
            Assert.Equal(expected, result);
        }
        [Fact]
        public void GeneratorGetPriorities_ShouldWorkOnTestData()
        {
            var expected = new List<string>{"Blocker","Critical","Major","Medium","Minor","Total","Unresolved"};
            var generator = new SummaryGenerator(new CsvReader("bugs-2002.csv").Read());
            var result = generator.Priorities;
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void GeneratorSummary_ShouldEqualsTestData()
        {
            var expected =
                ",TEAM_BEAUJOLAIS,TEAM_BORDEAUX,TEAM_BURGUNDY,TEAM_LOIRE,TEAM_PROVENCE,TEAM_RHONE,MISC,Total" +
                "Blocker,6,1,0,6,1,1,0,15" +
                "Critical,10,4,1,3,1,2,0,21" +
                "Major,15,5,6,19,0,2,0,47" +
                "Medium,0,0,0,3,0,0,1,4" +
                "Minor,0,0,1,1,1,0,0,3" +
                "Total,31,10,8,32,3,5,1,90" +
                "Unresolved,24,6,8,17,1,4,0,60";
            var generator = new SummaryGenerator(new CsvReader("bugs-2002.csv").Read());
            var result = "";
            foreach (var team in generator.Teams)
            {
                result += $",{team}";
            }
            foreach (var (key, value) in generator.Summary)
            {
                result += $"{key}";
                foreach (var col in value)
                {
                    result += $",{col.Value}";
                }
            }
            Assert.Equal(expected, result);
        }
    }
}