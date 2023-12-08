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
var startingNodes = network.Keys.Where(key => key.Last() == 'A').ToList();
var currentNodes = startingNodes;
var currentInstructions = instructions;
var endedInZ = new long[currentNodes.Count];

while (currentNodes.Any(node => node != ""))
{
    var instruction = currentInstructions[0];
    currentInstructions = currentInstructions.Substring(1);

    currentNodes = currentNodes.Select(node => node == "" ? "" : instruction == 'L' ? network[node].Left : network[node].Right).ToList();

    steps++;

    for (var i = 0; i < currentNodes.Count; i++)
    {
        if (currentNodes[i].EndsWith('Z'))
        {
            endedInZ[i] = steps;
            currentNodes[i] = "";
        }
    }

    if (currentInstructions == string.Empty)
    {
        currentInstructions = instructions;
    }
}

var answer = LCM(endedInZ);

Console.ReadKey();

static long LCM(params long[] numbers)
{
    return numbers.Aggregate(lcmOfTwo);
}
static long lcmOfTwo(long a, long b)
{
    return Math.Abs(a * b) / GCD(a, b);
}
static long GCD(long a, long b)
{
    return b == 0 ? a : GCD(b, a % b);
}

record Path(string Left, string Right);