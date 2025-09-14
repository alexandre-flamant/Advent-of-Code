class Program
{
    public static void Main(String[] args)
    {
        Dictionary<int, HashSet<int>> orderRule = new Dictionary<int, HashSet<int>>();
        List<List<int>> sequences = new List<List<int>>();
        using (StreamReader reader = new StreamReader(args[0]))
        {
            String? line = reader.ReadLine();
            while (line != null & !String.Equals("", line))
            {
                int[] order = line.Split('|').Select(int.Parse).ToArray();
                int key = order[1];
                int value = order[0];

                if (!orderRule.ContainsKey(key))
                    orderRule[key] = new HashSet<int>();

                orderRule[key].Add(value);

                line = reader.ReadLine();
            }

            line = reader.ReadLine();
            while (line != null)
            {
                List<int> sequence = line.Split(',').Select(int.Parse).ToList();
                sequences.Add(sequence);

                line = reader.ReadLine();
            }
        }

        List<bool> validity;
        bool fixSequence = false;
        if (args.Length > 1) fixSequence = (args[1] == "invalid");
        if (fixSequence)
        {
            validity = sequences.Select(sequence => !IsValid(sequence, orderRule)).ToList();
            for (int i = 0; i < sequences.Count; i++)
            {
                List<int> sequence = sequences[i];
                Validate(ref sequence, orderRule);
            }
        }
        else
            validity = sequences.Select(sequence => IsValid(sequence, orderRule)).ToList();

        int count = 0;
        for (int i = 0; i < sequences.Count; i++)
        {
            List<int> sequence = sequences[i];
            if (!validity[i]) continue;
            count += sequence[sequence.Count / 2];
        }

        Console.Write("Sum of values of mid valid sequences: {0}", count);
    }

    private static bool IsValid(List<int> sequence, Dictionary<int, HashSet<int>> orderRule)
    {
        for (int i = 0; i < sequence.Count - 1; i++)
            for (int j = i + 1; j < sequence.Count; j++)
            {
                if (!orderRule.ContainsKey(sequence[i])) continue;
                if (orderRule[sequence[i]].Contains(sequence[j])) return false;
            }

        return true;
    }

    private static void Validate(ref List<int> sequence, Dictionary<int, HashSet<int>> orderRule)
    {
        while (!IsValid(sequence, orderRule))
        {
            for (int i = 0; i < sequence.Count - 1; i++)
                for (int j = i + 1; j < sequence.Count; j++)
                {
                    if (!orderRule.ContainsKey(sequence[i])) continue;
                    if (orderRule[sequence[i]].Contains(sequence[j]))
                    {
                        int temp = sequence[j];
                        sequence[j] = sequence[i];
                        sequence[i] = temp;

                        Validate(ref sequence, orderRule);
                        continue;
                    }
                }
        }
        return;
    }
}
