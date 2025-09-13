class Program
{
    public static void Main(String[] args)
    {
        var path = args[0];
        String[] lines = File.ReadAllLines(path);

        List<List<int>> data = lines.Select(
                line => line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList()
                )
            .Where(row => row.Count > 1)
            .ToList();


        bool[] isSafe = data.Select(x => IsSafe(x, 0)).ToArray();
        int safeCount = isSafe.Aggregate(0, (acc, safe) => acc + (safe ? 1 : 0));

        Console.WriteLine("Number of safe records: {0}", safeCount);

        isSafe = data.Select(x => IsSafe(x, 1)).ToArray();
        safeCount = isSafe.Aggregate(0, (acc, safe) => acc + (safe ? 1 : 0));
        Console.WriteLine("Number of safe records with dampening of 1: {0}", safeCount);
    }


    public static bool IsSafe(List<int> data, int damp)
    {
        int slope = data[0] < data[1] ? 1 : -1;
        for (int i = 0; i < data.Count - 1; i++)
        {
            int delta = data[i + 1] - data[i];
            if (delta * slope < 1 || delta * slope > 3)
            {
                damp--;
                if (damp < 0) return false;
            }
        }
        return true;
    }
}
