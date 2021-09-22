using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace BugsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvReader reader;
            while (true)
            {
                Console.Write("Read from: ");
                string readPath = Console.ReadLine();
                try
                {
                    reader = new CsvReader(readPath);
                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found.");
                    Console.WriteLine("Please try again.");
                }
            }

            CsvWriter writer;
            while (true)
            {
                Console.Write("Write to: ");
                string writePath = Console.ReadLine();
                try
                {
                    writer = new CsvWriter(writePath);
                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found.");
                    Console.WriteLine("Please try again.");
                }
            }

            var parser = new BugsParser(reader, writer);
            parser.Parse();
            Console.WriteLine("Success!");
        }
    }
}