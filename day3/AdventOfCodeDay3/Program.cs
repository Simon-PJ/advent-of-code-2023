var input = File.ReadAllLines("input.txt");

var sumOfGearRatios = 0;

for (var j = 0; j < input.Length; j++)
{
    for (var i = 0; i < input[0].Length; i++)
    {
        if (input[j][i] == '*')
        {
            var gearNumbers = new List<int>();

            if (char.IsDigit(input[j][i - 1]))
            {
                var gearNumber = string.Join("", input[j].Take(i).Reverse().TakeWhile(char.IsAsciiDigit).Reverse());
                gearNumbers.Add(int.Parse(gearNumber));
            }
            if (char.IsDigit(input[j][i + 1]))
            {
                var gearNumber = string.Join("", input[j].Skip(i + 1).TakeWhile(char.IsDigit));
                gearNumbers.Add(int.Parse(gearNumber));
            }

            for (var j2 = -1; j2 <= 1; j2 += 2)
            {
                var number = "";
                var isGearNumber = false;

                for (var i2 = 0; i2 < input[0].Length; i2++)
                {
                    var j2Offset = j + j2;

                    if (j2Offset < 0 && j2Offset >= input.Length)
                    {
                        continue;
                    }

                    if (char.IsDigit(input[j2Offset][i2]))
                    {
                        number += input[j2Offset][i2];

                        if (Math.Abs(i2 - i) <= 1)
                        {
                            isGearNumber = true;
                        }
                    }

                    if (!char.IsDigit(input[j2Offset][i2]) || i2 + 1 == input[0].Length)
                    {
                        if (isGearNumber)
                        {
                            gearNumbers.Add(int.Parse(number));
                        }

                        number = "";
                        isGearNumber = false;
                    }
                }
            }

            if (gearNumbers.Count > 1)
            {
                if (gearNumbers.Contains(17))
                {

                }
                sumOfGearRatios += gearNumbers.Aggregate((a, b) => a * b);
            }
        }
    }
}

Console.WriteLine(sumOfGearRatios);
Console.ReadKey();