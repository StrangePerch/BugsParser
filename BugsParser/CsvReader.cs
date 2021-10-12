using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace BugsParser
{
    public class CsvReader : IReader
    {
        private readonly string _filepath;
        public CsvReader(string filepath)
        {
            _filepath = filepath;
            if (!File.Exists(_filepath)) throw new FileNotFoundException();
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