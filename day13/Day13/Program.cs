using System.Reflection.Metadata;

var input = File.ReadAllLines("input.txt");

var patterns = new List<string[]>();

var currentPattern = new List<string>();
foreach (var line in input)
{
    if (string.IsNullOrEmpty(line))
    {
        patterns.Add(currentPattern.ToArray());
        currentPattern = new List<string>();
        continue;
    }
    
    currentPattern.Add(line);
}
patterns.Add(currentPattern.ToArray());

int GetVerticalReflection(string[] pattern)
{
    for (var i = 1; i < pattern[0].Length; i++)
    {
        var eachSide = Math.Min(i, pattern[0].Length - i);

        var differences = 0;

        for (var j = 0; j < pattern.Length; j++)
        {
            var left = pattern[j].Substring(i - eachSide, eachSide);
            var right = pattern[j].Substring(i, eachSide);

            differences += Differences(left, string.Join("", right.Reverse()));
        }

        if (differences == 1)
        {
            return i;
        }
    }

    return 0;
}

int GetHorizontalReflection(string[] pattern)
{
    for (var j = 1; j < pattern.Length; j++)
    {
        var eachSide = Math.Min(j, pattern.Length - j);

        var differences = 0;

        for (var i = 0; i < pattern[0].Length; i++)
        {
            var above = string.Join("", Enumerable.Range(j - eachSide, eachSide).Select(y => pattern[y][i]));
            var below = string.Join("", Enumerable.Range(j, eachSide).Select(y => pattern[y][i]));

            differences += Differences(above, string.Join("", below.Reverse()));
        }

        if (differences == 1)
        {
            return j;
        }
    }

    return 0;
}

int Differences(string a, string b)
{
    int differences = 0;

    for (var i = 0; i < a.Length; i++)
    {
        if (a[i] != b[i]) differences++;
    }

    return differences;
}

int answer = 0;
foreach (var pattern in patterns)
{
    var vertical = GetVerticalReflection(pattern);
    var horizontal = GetHorizontalReflection(pattern);

    answer += vertical + (horizontal * 100);
}

Console.ReadLine();