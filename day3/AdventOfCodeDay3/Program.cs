var input = File.ReadAllLines("input.txt");

var sumOfPartNumbers = 0;

for (var j = 0; j < input.Length; j++)
{
    var number = "";
    var isPartNumber = false;
    for (var i = 0; i < input[0].Length; i++)
    {
        if (char.IsDigit(input[j][i]))
        {
            number += input[j][i];

            if (!isPartNumber)
            {
                for (var i2 = -1; i2 <= 1; i2++)
                {
                    for (var j2 = -1; j2 <= 1; j2++)
                    {
                        var offseti = i + i2;
                        var offsetj = j + j2;
                        if (offseti >= 0 && offseti < input[0].Length && offsetj >= 0 && offsetj < input.Length && input[offsetj][offseti] != '.' && !char.IsDigit(input[offsetj][offseti]))
                        {
                            isPartNumber = true;
                        }
                    }
                }
            }
        }
        
        if (!char.IsDigit(input[j][i]) || i + 1 == input[0].Length)
        {
            if (isPartNumber)
            {
                sumOfPartNumbers += int.Parse(number);
            }

            number = "";
            isPartNumber = false;
        }
    }
}

Console.WriteLine(sumOfPartNumbers);
Console.ReadKey();