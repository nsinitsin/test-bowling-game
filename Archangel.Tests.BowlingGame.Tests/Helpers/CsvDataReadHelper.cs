using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Archangel.Tests.BowlingGame.Tests.Helpers
{
    public static class CsvDataReadHelper
    {
        public static IEnumerable<object[]> GetData(string fileName)
        {
            string executableLocation = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            var _fileName = Path.Combine(executableLocation, fileName);

            using (var csvFile = new StreamReader(_fileName))
            {
                string line;
                while ((line = csvFile.ReadLine()) != null)
                {
                    var row = line.Split(',');
                    yield return row;
                }
            }
        }
    }
}
