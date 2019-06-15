using System.Collections.Generic;
using System.IO;

namespace Common.Creators
{
    public sealed class CreatorRawMsdc : ICreator
    {
        public IEnumerable<int> Create()
        {
            var trackIds = new HashSet<int>();

            using (var reader = new StreamReader(File.Open(@"E:\temp\facts-nns.csv", FileMode.Open)))
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