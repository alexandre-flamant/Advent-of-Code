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
            .Where(row => row.Count > 0)
            .ToList();

        foreach (int damp in new int[] { 0, 1 })
        {
            bool[] isSafe = data.Select(x => IsSafe(x, damp)).ToArray();
            int safeCount = isSafe.Aggregate(0, (acc, safe) => acc + (safe ? 1 : 0));

            Console.WriteLine("Number of safe records with damp {0}: {1}", damp, safeCount);
        }
    }

    public static bool IsSafe(List<int> data, int damp)
    {
        if (data.Count < 2) return false;

        // Arithmetic check for the slope continuity
        int slope = data[0] < data[1] ? 1 : -1;
        for (int i = 0; i < data.Count - 1; i++)
        {
            int delta = data[i + 1] - data[i];
            if ((delta * slope < 1) || (delta * slope) > 3)
            {
                // Avoid unnecessary copy
                if (damp == 0) return false;

                // Edge case for the very first item of the list
                foreach (int j in i == 1 ? new int[] { -1, 0, 1 } : new int[] { 0, 1 })
                {
                    List<int> dataCopy = new List<int>(data);

                    dataCopy.RemoveAt(i + j);
                    bool isSafe = IsSafe(dataCopy, damp - 1);

                    if (isSafe) return true;
                }
                return false;
            }
        }
        return true;
    }
}
