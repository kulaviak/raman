using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raman.Core
{
    public class OnePointPerLineFileReader
    {
        private readonly string _filePath;

        private readonly List<string> _ignoredLines = new List<string>();

        public List<string> IgnoredLines => _ignoredLines;

        public OnePointPerLineFileReader(string filePath)
        {
            _filePath = filePath;
        }
        
        public List<Point> TryReadFile()
        {
            List<string> lines;
            try
            {
                lines = File.ReadLines(_filePath).ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var ret = TryParseLines(lines);
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception($"Loading file {_filePath} failed.", ex);
            }
        }

        private List<Point> TryParseLines(List<string> lines)
        {
            var ret = new List<Point>();
            try
            {
                foreach (var line in lines)
                {
                    var point = TryParseLine(line);
                    if (point != null)
                    {
                        ret.Add(point);
                    }
                    else
                    {
                        _ignoredLines.Add(line);
                    }
                }
                return ret;
            }
            catch (Exception e)
            {
                // ignore
                return null;
            }      
        }

        public static Point TryParseLine(string line)
        {
            try
            {
                // split on white space or tab
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    if (Decimal.TryParse(parts[0], out decimal x) && Decimal.TryParse(parts[1], out decimal y))
                    {
                        return new Point(x, y);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}