using System.Drawing;

var input = File.ReadAllLines("input.txt");

var pipes = new Dictionary<string, Point>()
{
    { "|,0,1", new Point(0, 1) },
    { "|,0,-1", new Point(0, -1) },

    { "-,1,0", new Point(1, 0) },
    { "-,-1,0", new Point(-1, 0) },

    { "L,0,1", new Point(1, 0) },
    { "L,-1,0", new Point(0, -1) },

    { "J,0,1", new Point(-1, 0) },
    { "J,1,0", new Point(0, -1) },

    { "7,1,0", new Point(0, 1) },
    { "7,0,-1", new Point(-1, 0) },

    { "F,-1,0", new Point(0, 1) },
    { "F,0,-1", new Point(1, 0) },
};

Point FindStartPosition(string[] input)
{
    for (var j = 0; j < input.Length; j++)
    {
        for (var i = 0; i < input[j].Length; i++)
        {
            if (input[j][i] == 'S')
            {
                return new Point(i, j);
            }
        }
    }

    throw new Exception("Couldn't find start");
}

char WorkoutStartPipe(string[] input, Point start)
{
    var adjacentPipes = new Point[] { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };

    var attachedPipes = adjacentPipes.Where(pipe =>
    {
        var x = start.X + pipe.X;
        var y = start.Y + pipe.Y;

        if (x < 0 || x >= input[0].Length || y < 0 || y >= input.Length)
            return false;

        var key = input[y][x];
        return pipes.ContainsKey($"{key},{pipe.X},{pipe.Y}");
    });

    if (attachedPipes.Contains(adjacentPipes[0]) && attachedPipes.Contains(adjacentPipes[1]))
    {
        return 'L';
    }
    else if (attachedPipes.Contains(adjacentPipes[0]) && attachedPipes.Contains(adjacentPipes[2]))
    {
        return '|';
    }
    else if (attachedPipes.Contains(adjacentPipes[0]) && attachedPipes.Contains(adjacentPipes[3]))
    {
        return 'J';
    }
    else if (attachedPipes.Contains(adjacentPipes[1]) && attachedPipes.Contains(adjacentPipes[2]))
    {
        return 'F';
    }
    else if (attachedPipes.Contains(adjacentPipes[1]) && attachedPipes.Contains(adjacentPipes[3]))
    {
        return '-';
    }
    else if (attachedPipes.Contains(adjacentPipes[2]) && attachedPipes.Contains(adjacentPipes[3]))
    {
        return '7';
    }

    return ',';
    throw new Exception("You messed up somehow");
}

List<Point> GetPoints(string[] input, Point start)
{
    var startPipe = input[start.Y][start.X];
    var firstMove = pipes[pipes.Keys.First(key => key.StartsWith(startPipe))];

    Point currentPoint = new Point(start.X + firstMove.X, start.Y + firstMove.Y);
    List<Point> points = new List<Point>();
    Point lastMove = new Point(firstMove.X, firstMove.Y);

    points.Add(currentPoint);

    while (currentPoint != start)
    {
        var currentPipe = input[currentPoint.Y][currentPoint.X];
        var key = $"{currentPipe},{lastMove.X},{lastMove.Y}";
        var move = pipes[key];

        lastMove.X = move.X;
        lastMove.Y = move.Y;

        currentPoint.X += move.X;
        currentPoint.Y += move.Y;

        points.Add(currentPoint);
    }

    return points;
}

bool IsPointInPolygon(Point[] polygon, Point testPoint)
{
    bool result = false;
    int j = polygon.Length - 1;
    for (int i = 0; i < polygon.Length; i++)
    {
        if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y ||
            polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
        {
            if (polygon[i].X + (testPoint.Y - polygon[i].Y) /
               (polygon[j].Y - polygon[i].Y) *
               (polygon[j].X - polygon[i].X) < testPoint.X)
            {
                result = !result;
            }
        }
        j = i;
    }
    return result;
}

int EnclosedCount(Point[] loop, string[] input)
{
    int count = 0;

    for (var i = 0; i < input[0].Length - 1; i++)
    {
        for (var j = 0; j < input.Length; j++)
        {
            if (!loop.Contains(new Point(i, j)) && IsPointInPolygon(loop, new Point(i, j)))
            {
                count++;
            }
        }
    }

    return count;
}

var start = FindStartPosition(input);
var startPipe = WorkoutStartPipe(input, start);

input[start.Y] = input[start.Y].Replace('S', startPipe);

var polygon = GetPoints(input, start).ToArray();
var enclosedCount = EnclosedCount(polygon, input);

Console.ReadLine();
