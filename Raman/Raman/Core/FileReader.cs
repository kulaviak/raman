using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raman.Core
{
    public class FileReader
    {
        public List<DataPoint> ReadFile(string filePath)
        {
            List<string> lines;
            try
            {
                lines = File.ReadLines(filePath).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Loading file {filePath} failed.", ex);
            }
            try
            {
                
            }
        }
    }
}