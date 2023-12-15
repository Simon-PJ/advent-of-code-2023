var input = File.ReadAllLines("input.txt");

const char Empty = '.';
const char RoundRock = 'O';
const char SquareRock = '#';

var totalLoad = 0;

for (var i = 0; i < input[0].Length; i++)
{
    var tiltedLine = input[0][i].ToString();

    for (var j = 1; j < input.Length; j++)
    {
        if (input[j][i] == RoundRock && tiltedLine[j - 1] == Empty)
        {
            var drop = tiltedLine.LastIndexOfAny(new[] { SquareRock, RoundRock });
            tiltedLine = tiltedLine.Substring(0, drop + 1)
                + input[j][i]
                + tiltedLine.Substring(drop + 1);
        }
        else
        {
            tiltedLine += input[j][i];
        }
    }

    var lineLoad = tiltedLine.Select((x, i) =>
    {
        var wregr = x == RoundRock ? tiltedLine.Length - i : 0;
        return x == RoundRock ? tiltedLine.Length - i : 0;
    }).Sum();

    totalLoad += lineLoad;
}

Console.ReadLine();