using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace BugsParser
{
    public class CsvReader : IReader
    {
        private string _filepath;
        public CsvReader(string filepath)
        {
            _filepath = filepath;
        }
        public List<string[]> Read()
        {
            var list = new List<string[]>();

            var parser = new TextFieldParser(_filepath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                list.Add(fields);
            }

            return list;
        }
    }
}