var input = File.ReadAllLines("input.txt");

const char Empty = '.';
const char RoundRock = 'O';
const char SquareRock = '#';

var totalLoad = 0;

string[] TiltNorth()
{
    var tiltedGrid = new string[input.Length];
    for (var j = 0; j < input.Length; j++) tiltedGrid[j] = "";

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

        for (var j = 0; j < input.Length; j++) tiltedGrid[j] += tiltedLine[j];
    }

    return tiltedGrid;
}

//string[] TiltSouth()
//{
//    var tiltedGrid = new string[input.Length];
//    for (var j = 0; j < input.Length; j++) tiltedGrid[j] = "";

//    for (var i = 0; i < input[0].Length; i++)
//    {
//        var tiltedLine = input[0][i].ToString();

//        for (var j = inputl; j < input.Length; j++)
//        {
//            if (input[j][i] == RoundRock && tiltedLine[j - 1] == Empty)
//            {
//                var drop = tiltedLine.LastIndexOfAny(new[] { SquareRock, RoundRock });
//                tiltedLine = tiltedLine.Substring(0, drop + 1)
//                    + input[j][i]
//                    + tiltedLine.Substring(drop + 1);
//            }
//            else
//            {
//                tiltedLine += input[j][i];
//            }
//        }

//        for (var j = 0; j < input.Length; j++) tiltedGrid[j] += tiltedLine[j];
//    }

//    return tiltedGrid;
//}

string[] RotateLeft(string[] a, int d)
{

    Queue<string> queue = new Queue<string>(a);
    Stack<string> stack = new Stack<string>();

    while (d > 0)
    {
        stack.Push(queue.Dequeue());
        queue.Enqueue(stack.Pop());
        d--;
    }

    return queue.ToArray();
}

bool SameAs(string[] a, string[] b)
{
    for (var i = 0; i < a.Length; i++)
    {
        for (var j = 0; j < a[0].Length; j++)
        {
            if (a[j][i] != b[j][i]) return false;
        }
    }

    return true;
}

int iteration = 0;
int cycles = 0;
string[] lastEasty = input;
bool exit = false;
while (!exit)
{
    switch (iteration)
    {
        case 0:
            // North
            input = TiltNorth();
            break;
        case 1:
            // West
            input = RotateLeft(input, 3);
            TiltNorth();
            input = RotateLeft(input, 1);
            break;
        case 2:
            // South
            input = RotateLeft(input, 2);
            TiltNorth();
            input = RotateLeft(input, 2);
            break;
        case 3:
            // East
            input = RotateLeft(input, 1);
            TiltNorth();
            input = RotateLeft(input, 3);

            if (SameAs(lastEasty, input))
            {
                exit = true;
            }

            lastEasty = input;
            break;
        default:
            throw new Exception("Oops");
    }
    input = TiltNorth();

    iteration++;
    cycles++;
    iteration = iteration > 3 ? 0 : iteration;
}

Console.ReadLine();