﻿using System.Drawing;
using System.Text;

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

    throw new Exception("You messed up somehow");
}

int GetLoopLength(string[] input, Point start)
{
    var startPipe = input[start.Y][start.X];
    var firstMove = pipes[pipes.Keys.First(key => key.StartsWith(startPipe))];

    Point currentPoint = new Point(start.X + firstMove.X, start.Y + firstMove.Y);
    int length = 1;
    Point lastMove = new Point(firstMove.X, firstMove.Y);
    
    while (currentPoint != start)
    {
        var currentPipe = input[currentPoint.Y][currentPoint.X];
        var key = $"{currentPipe},{lastMove.X},{lastMove.Y}";
        var move = pipes[key];

        lastMove.X = move.X;
        lastMove.Y = move.Y;

        currentPoint.X += move.X;
        currentPoint.Y += move.Y;
        
        length++;
    }

    return length;
}

string ReplaceAt(string s, int at, char replaceWith)
{
    var sb = new StringBuilder(s);
    sb[at] = replaceWith;
    return sb.ToString();
}

void BuildPipeMap(Point start)
{
    var startPipe = input[start.Y][start.X];
    var firstMove = pipes[pipes.Keys.First(key => key.StartsWith(startPipe))];

    Point currentPoint = new Point(start.X + firstMove.X, start.Y + firstMove.Y);
    Point lastMove = new Point(firstMove.X, firstMove.Y);

    input[start.Y] = ReplaceAt(input[start.Y], start.X, 'P');

    while (currentPoint != start)
    {
        var currentPipe = input[currentPoint.Y][currentPoint.X];
        var key = $"{currentPipe},{lastMove.X},{lastMove.Y}";
        var move = pipes[key];

        input[currentPoint.Y] = ReplaceAt(input[currentPoint.Y], currentPoint.X, 'P');

        lastMove.X = move.X;
        lastMove.Y = move.Y;

        currentPoint.X += move.X;
        currentPoint.Y += move.Y;
    }
}

List<Point> visited = new List<Point>();

bool IsEnclosed(string[] input, Point p)
{
    if (visited.Contains(p)) return true;
    if (p.Y < 0 || p.Y >= input.Length || p.X < 0 || p.X >= input[0].Length) return false;

    visited.Add(p);

    var tile = input[p.Y][p.X];
    var validNeighbours = new[] { 'P', '.' };

    if (tile != '.')
    {
        return validNeighbours.Contains(tile);
    }
    else
    {
        var north = IsEnclosed(input, new Point(p.X, p.Y - 1));
        var south = IsEnclosed(input, new Point(p.X, p.Y + 1));
        var east = IsEnclosed(input, new Point(p.X + 1, p.Y));
        var west = IsEnclosed(input, new Point(p.X - 1, p.Y));

        return north && south && east && west;
    }
}

int GetEnclosedTileCount(string[] input)
{
    int count = 0;
    var enclosed = new List<Point>();

    for (var j = 0; j < input.Length; j++)
    {
        for (var i = 0; i < input[0].Length; i++)
        {
            visited = new List<Point>();
            if (input[j][i] == '.' && IsEnclosed(input, new Point(i, j)))
            {
                enclosed.Add(new Point(i, j));
                count++;
            }
        }
    }

    return count;
}

var start = FindStartPosition(input);
var startPipe = WorkoutStartPipe(input, start);

input[start.Y] = input[start.Y].Replace('S', startPipe);

var loopLength = GetLoopLength(input, start);
var furthest = Math.Floor((double)loopLength / 2);

BuildPipeMap(start);
var enclosedTileCount = GetEnclosedTileCount(input);

Console.ReadLine();