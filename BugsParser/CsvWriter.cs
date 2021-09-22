using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace BugsParser
{
    public class CsvWriter : IWriter
    {
        private readonly string _filepath;
        public CsvWriter(string filepath)
        {
            _filepath = filepath;
        }
        public void Write(Dictionary<string, Dictionary<string, int>> summary, List<string> teams)
        {
            var stream = new StreamWriter(_filepath, false, System.Text.Encoding.Default);
            foreach (var team in teams)
            {
                stream.Write($",{team}");
            }
            stream.WriteLine();
            foreach (var (key, value) in summary)
            {
                stream.Write($"{key}");
                foreach (var col in value)
                {
                    stream.Write($",{col.Value}");
                }
                stream.WriteLine();
            }
            stream.Close();
        }
    }
}