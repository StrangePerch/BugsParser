using System;
using System.IO;
using System.Linq;
using Xunit;

namespace BugsParser.Tests
{
    public class ReaderTests
    {
        [Fact]
        public void Read_ShouldReturnExpectedAmountOfRows()
        {
            var expected = 91;
            var reader = new CsvReader("bugs-2002.csv");
            var result = reader.Read().Count;
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Read_ShouldReturnExpectedAmountOfColumns()
        {
            var expected = 14;
            var reader = new CsvReader("bugs-2002.csv");
            var result = reader.Read().First().Length;
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Read_ShouldThrowExceptionOnWrongPath()
        {
            var reader = new CsvReader("wrongPath.csv");
            Assert.Throws<FileNotFoundException>(() => reader.Read());
        }
    }
}