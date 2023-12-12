var input = File.ReadAllLines("input.txt");

const char Unknown = '?';
const char Operational = '.';
const char Damaged = '#';

var lines = input.Select(x =>
{
    var springs = x.Split(' ')[0];
    var arrangement = x.Split(' ')[1].Split(',').Select(int.Parse).ToArray();
    return new Line(springs, arrangement);
}).ToArray();

string[] Combos(string springs)
{
    if (!springs.Contains(Unknown))
        return new[] { springs };

    var firstUnknown = springs.IndexOf(Unknown);

    var asOperational = springs.Substring(0, firstUnknown) + Operational + springs.Substring(firstUnknown + 1);
    var asDamaged = springs.Substring(0, firstUnknown) + Damaged + springs.Substring(firstUnknown + 1);

    return new[] { asOperational, asDamaged }.SelectMany(x => Combos(x)).ToArray();
}

bool Valid(string springs, int[] arrangement)
{
    var actualArrangement = springs.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length);
    return Enumerable.SequenceEqual(actualArrangement, arrangement);
}

var allCombos = lines.Select(line =>
{
    var combos = Combos(line.Springs).ToArray();

    var validCombos = combos.Where(x => Valid(x, line.Arrangement)).ToArray();

    return validCombos.Length;
}).ToArray();

var sumCombos = allCombos.Sum();

Console.ReadLine();

record Line(string Springs, int[] Arrangement);
