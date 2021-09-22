namespace BugsParser
{
    public class BugsParser
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;
        
        public BugsParser(IReader reader, IWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }
        public void Parse()
        {
            var generator = new SummaryGenerator(_reader.Read());
            _writer.Write(generator.Summary, generator.Teams);
        }
    }
}