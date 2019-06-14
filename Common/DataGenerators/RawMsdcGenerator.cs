using System.Collections.Generic;
using System.IO;

namespace Common.DataGenerators
{
    public sealed class RawMsdcGenerator : IDataGenerator
    {
        public IEnumerable<int> Generate()
        {
            var trackIds = new HashSet<int>();

            using (var reader = new StreamReader(File.Open(@"D:\temp\facts-nns.csv", FileMode.Open)))
            {
                _ = reader.ReadLine();

                var line = reader.ReadLine();

                while (line != null)
                {
                    var trackId = int.Parse(line.Split(',')[1]);

                    trackIds.Add(trackId);

                    line = reader.ReadLine();
                }
            }

            return trackIds;
        }

        public override string ToString() => "Dane MSDC";
    }
}