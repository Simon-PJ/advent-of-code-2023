using System.Runtime.CompilerServices;

var input = File.ReadAllLines("input.txt");

var instructions = input[0];

var network = new Dictionary<string, Path>();

for (var i = 2; i < input.Length; i++)
{
    var parts = input[i].Split(" = ");
    var key = parts[0];

    var directionParts = parts[1].Split(", ");
    var left = directionParts[0].TrimStart('(');
    var right = directionParts[1].TrimEnd(')');

    network.Add(key, new Path(left, right));
}

var steps = 0;
var currentNode = "AAA";
var destinationNode = "ZZZ";
var currentInstructions = instructions;

while (currentNode != destinationNode)
{
    var instruction = currentInstructions[0];
    currentInstructions = currentInstructions.Substring(1);

    currentNode = instruction == 'L' ? network[currentNode].Left : network[currentNode].Right;
    steps++;
    
    if (currentInstructions == string.Empty)
    {
        currentInstructions = instructions;
    }
}

Console.ReadKey();

record Path(string Left, string Right);