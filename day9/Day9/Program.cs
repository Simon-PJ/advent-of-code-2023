var input = File.ReadAllLines("input.txt");

var parsedLines = input.Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();

var result = parsedLines.Sum(GetNextInLine);

int GetNextInLine(List<int> line)
{
    var nextLine = new List<int>();

    for (var i = 0; i < line.Count - 1; i++)
    {
        nextLine.Add(line[i + 1] - line[i]);
    }

    if (nextLine.All(x => x == 0))
    {
        return line.Last();
    }
    else
    {
        return line.Last() + GetNextInLine(nextLine);
    }
}

Console.ReadLine();