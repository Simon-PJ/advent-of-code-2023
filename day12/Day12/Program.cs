var input = File.ReadAllLines("input.txt");

const char Unknown = '?';
const char Operational = '.';
const char Damaged = '#';
const int RepeatLine = 5;

var lines = input.Select(x =>
{
    var springs = x.Split(' ')[0];
    var arrangement = x.Split(' ')[1].Split(',').Select(int.Parse).ToArray();
    return new Line(springs, arrangement);
}).ToArray();

//string[] AllCombos(string springs)
//{
//    if (!springs.Contains(Unknown))
//        return new[] { springs };

//    var firstUnknown = springs.IndexOf(Unknown);

//    var asOperational = springs.Substring(0, firstUnknown) + Operational + springs.Substring(firstUnknown + 1);
//    var asDamaged = springs.Substring(0, firstUnknown) + Damaged + springs.Substring(firstUnknown + 1);

//    return new[] { asOperational, asDamaged }.SelectMany(x => AllCombos(x)).ToArray();
//}

bool Valid(string springs, int[] arrangement)
{
    var actualArrangement = springs.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length);
    return Enumerable.SequenceEqual(actualArrangement, arrangement);
}

bool IsPossibleArrangement(string springs, int[] arrangement)
{
    var actualArrangement = springs.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).ToArray();

    if (actualArrangement.Length > arrangement.Length)
        return false;

    if (actualArrangement.Length == 0)
        return true;

    for (var i = 0; i < actualArrangement.Length - 1; i++)
    {
        if (arrangement[i] != actualArrangement[i])
            return false;
    }

    return actualArrangement.Last() <= arrangement[actualArrangement.Length - 1];
}

string[] ValidCombos(string springs, int[] arrangement)
{
    if (!springs.Contains(Unknown))
        return Valid(springs, arrangement) ? new[] { springs } : new string[] { } ;

    var firstUnknown = springs.IndexOf(Unknown);

    var asOperational = springs.Substring(0, firstUnknown) + Operational + springs.Substring(firstUnknown + 1);
    var asDamaged = springs.Substring(0, firstUnknown) + Damaged + springs.Substring(firstUnknown + 1);

    var validSprings = new List<string>();

    if (IsPossibleArrangement(asOperational.Substring(0, firstUnknown + 1), arrangement))
        validSprings.Add(asOperational);

    if (IsPossibleArrangement(asDamaged.Substring(0, firstUnknown + 1), arrangement))
        validSprings.Add(asDamaged);

    return validSprings.SelectMany(x => ValidCombos(x, arrangement)).ToArray();
}

var allCombos = lines.Select((line, i) =>
{
    Console.WriteLine((i + 1) + "/" + lines.Length);

    var repeatedLine = string.Join("?", Enumerable.Repeat(line.Springs, RepeatLine));
    var repeatedArrangement = Enumerable.Repeat(line.Arrangement, RepeatLine).SelectMany(x => x).ToArray();
    var combos = ValidCombos(repeatedLine, repeatedArrangement).ToArray();

    return combos.Length;
}).ToArray();

var sumCombos = allCombos.Sum();

Console.ReadLine();

record Line(string Springs, int[] Arrangement);
