using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.DataGenerators
{
    public sealed class RandomMsdcGenerator : IDataGenerator
    {
        private readonly Random _random = new Random();

        public IEnumerable<int> Generate()
        {
            var ids = new List<(int UserId, int TrackId)>();

            using (var reader = new StreamReader(File.Open(@"D:\temp\facts-nns.csv", FileMode.Open)))
            {
                _ = reader.ReadLine();

                string line = reader.ReadLine();

                while (line != null)
                {
                    var rawIds = line.Split(',');

                    int userId = int.Parse(rawIds[0]);
                    int trackId = int.Parse(rawIds[1]);

                    ids.Add((userId, trackId));

                    line = reader.ReadLine();
                }
            }

            int count = ids.Select(x => x.UserId).Distinct().Count();

            return ids.GroupBy(x => x.UserId, x => x.TrackId).SelectMany(x => Shuffle(x.ToList()).Take(100));
        }


        private IEnumerable<T> Shuffle<T>(IList<T> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;

                int k = _random.Next(n + 1);

                T value = list[k];

                list[k] = list[n];

                list[n] = value;
            }

            return list;
        }
    }
}
