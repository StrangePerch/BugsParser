using System.Collections.Generic;

namespace BugsParser
{
    public interface IReader
    {
        public List<string[]> Read();
    }
    
    public interface IWriter
    {
        public void Write(Dictionary<string, Dictionary<string, int>> summary, List<string> teams);
    }
}