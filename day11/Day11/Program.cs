using System.Drawing;

const char Space = '.';
const char Galaxy = '#';

var input = File.ReadLines("input.txt").ToArray();

var tempInput = new List<string>();

for (var j = 0; j < input.Length; j++)
{
    tempInput.Add(input[j]);

    if (input[j].All(x => x == Space))
    {
        tempInput.Add(string.Join("", Enumerable.Repeat(Space, input[j].Length)));
    }
}

input = tempInput.ToArray();

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
        for (var j = 0; j < input.Length; j++)
        {
            input[j] = input[j].Substring(0, i) + Space + input[j].Substring(i);
        }
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

    var sumOfPaths = restOfGalaxies.Sum(galaxy => Math.Abs(galaxy.X - firstGalaxy.X) + Math.Abs(galaxy.Y - firstGalaxy.Y));
    var restOfSumOfPaths = CalculateSumOfPaths(restOfGalaxies);

    return sumOfPaths + restOfSumOfPaths;
}

var sumOfPaths = CalculateSumOfPaths(galaxies);

Console.ReadKey();