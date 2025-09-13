class Program
{
    static void Main(String[] args)
    {
        String path = args[0];
        var numbers = new List<List<int>>();

        String[] lines = File.ReadAllLines(path);
        List<int[]> rows = lines.Select(
                line => line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray()
                ).ToList();

        int[] leftList = rows.Select(row => row[0]).OrderBy(x => x).ToArray();
        int[] rightList = rows.Select(row => row[1]).OrderBy(x => x).ToArray();

        var leftCount = new Dictionary<int, int>();
        var rightCount = new Dictionary<int, int>();

        int distance = 0;
        for (int i = 0; i < leftList.Count(); i++)
        {
            int l = leftList[i];
            int r = rightList[i];

            distance += Math.Abs(l - r);

            leftCount[l] = leftCount.GetValueOrDefault(l, 0) + 1;
            rightCount[r] = rightCount.GetValueOrDefault(r, 0) + 1;
        }

        int similarity = 0;
        foreach (int value in leftCount.Keys)
            similarity += value * leftCount[value] * rightCount.GetValueOrDefault(value, 0);

        Console.WriteLine("Distance: {0}", distance);
        Console.WriteLine("Similarity {0}", similarity);
    }
}
