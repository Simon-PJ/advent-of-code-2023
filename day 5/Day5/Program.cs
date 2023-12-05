var input = File.ReadAllLines("input.txt");

var seeds = input[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

var maps = new List<List<Map>>();
var currentMaps = new List<Map>();

for (var i = 1; i < input.Length; i++)
{
    var line = input[i];

    if (string.IsNullOrEmpty(line) && currentMaps.Count > 0)
    {
        maps.Add(currentMaps);
        currentMaps = new List<Map>();
    }
    else if (char.IsDigit(line.Length > 0 ? line[0] : 'a'))
    {
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        currentMaps.Add(new Map(parts[0], parts[1], parts[2]));
    }
}
maps.Add(currentMaps);

var locations = seeds.Select(seed =>
{
    foreach (var map in maps)
    {
        var matchingMap = map.SingleOrDefault(x => seed >= x.SourceStart && seed <= x.SourceStart + x.Range - 1);

        if (matchingMap != null)
        {
            seed = matchingMap.DestStart + seed - matchingMap.SourceStart;
        }
    }

    return seed;
}).ToArray();

var lowestLocation = locations.Min();

Console.ReadKey();

record Map(long DestStart, long SourceStart, long Range);