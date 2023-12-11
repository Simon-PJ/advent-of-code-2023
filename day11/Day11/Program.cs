using System.Drawing;

const char Space = '.';
const char Galaxy = '#';
const int ExpansionFactor = 2;

var input = File.ReadLines("input.txt").ToArray();

var emptyRows = new List<int>();
var emptyColumns = new List<int>();

for (var j = 0; j < input.Length; j++)
{
    if (input[j].All(x => x == Space))
    {
        emptyRows.Add(j);
    }
}

for (var i = input[0].Length - 1; i >= 0; i--)
{
    bool allSpace = true;

    for (var j = 0; j < input.Length; j++)
    {
        if (input[j][i] != Space)
        {
            allSpace = false;
            break;
        }
    }

    if (allSpace)
    {
        emptyColumns.Add(i);
    }
}

var galaxies = new List<Point>();

for (var i = 0; i < input[0].Length; i++)
{
    for (var j = 0; j < input.Length; j++)
    {
        if (input[j][i] == Galaxy)
        {
            galaxies.Add(new Point(i, j));
        }
    }
}

int CalculateSumOfPaths(List<Point> galaxies)
{
    if (galaxies.Count == 1) return 0;
    var firstGalaxy = galaxies.First();
    var restOfGalaxies = galaxies.Skip(1).ToList();

    var sumOfPaths = restOfGalaxies.Sum(galaxy => {
        int expansionXMultiplier = 0;
        int expansionYMultiplier = 0;

        for (var i = Math.Min(galaxy.X, firstGalaxy.X); i <= Math.Max(galaxy.X, galaxy.X); i++)
        {
            if (emptyColumns.Contains(i)) expansionXMultiplier++;
        }

        for (var j = Math.Min(galaxy.Y, firstGalaxy.Y); j <= Math.Max(galaxy.Y, galaxy.Y); j++)
        {
            if (emptyRows.Contains(j)) expansionYMultiplier++;
        }

        return
            Math.Abs(galaxy.X - firstGalaxy.X) + Math.Abs(galaxy.Y - firstGalaxy.Y)
             + (expansionXMultiplier * ExpansionFactor)
             + (expansionYMultiplier * ExpansionFactor)
             - expansionXMultiplier
             - expansionYMultiplier;
    });

    var restOfSumOfPaths = CalculateSumOfPaths(restOfGalaxies);

    return sumOfPaths + restOfSumOfPaths;
}

var sumOfPaths = CalculateSumOfPaths(galaxies);

Console.ReadKey();