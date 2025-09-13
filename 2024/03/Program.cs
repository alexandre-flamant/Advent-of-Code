using System.Text.RegularExpressions;
class Program
{
    public static void Main(String[] args)
    {
        String[] rows = File.ReadAllLines(args[0]);
        String operation = String.Join(' ', rows);
        String pattern = """don\'t\(\)|do\(\)|(mul\([0-9]{1,3},[0-9]{1,3}\))""";

        bool skip = false;
        int result = 0;
        foreach (Match match in Regex.Matches(operation, pattern))
        {
            switch (match.Value)
            {
                case "do()":
                    skip = false;
                    continue;

                case "don't()":
                    skip = true;
                    continue;

                default:
                    if (!skip)
                        result += ParseMult(match.Value);
                    break;
            }
        }

        Console.WriteLine("Sum of the multiplications: {0}", result);
    }

    static int ParseMult(String multiplication)
    {
        return multiplication
            .Substring(4, multiplication.Length - 5)
            .Split(',')
            .Select(int.Parse)
            .Aggregate(1, (x, y) => x * y);
    }
}
