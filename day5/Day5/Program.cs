var input = File.ReadAllLines("input.txt");

var seedInfo = input[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

var seeds = new List<Seed>();
for (var i = 0; i < seedInfo.Length; i+=2)
{
    seeds.Add(new Seed(seedInfo[i], seedInfo[i+1]));
}

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
maps.Reverse();

long location = 0;
bool foundMinLocation = false;

while (!foundMinLocation)
{
    location++;

    var mappedValue = location;

    foreach (var map in maps)
    {
        var matchingMap = map.SingleOrDefault(x => mappedValue >= x.DestStart && mappedValue <= x.DestStart + x.Range - 1);

        if (matchingMap != null)
        {
            mappedValue = matchingMap.SourceStart + mappedValue - matchingMap.DestStart;
        }
    }

    if (seeds.Any(seed => mappedValue >= seed.Start && mappedValue < seed.Start + seed.Range))
    {
        foundMinLocation = true;
    }
}

Console.ReadKey();

record Map(long DestStart, long SourceStart, long Range);

record Seed(long Start, long Range);