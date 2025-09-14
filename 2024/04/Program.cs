class Program
{
    public static void Main(String[] args)
    {
        String[] data = File.ReadAllLines(args[0]);

        char[][] grid = data.Select(s => s.ToCharArray()).ToArray();

        int n = grid.Length; int m = grid[0].Length;
        int count = 0;

        int problem = int.Parse(args[1]);

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (problem == 0)
                {
                    if (grid[i][j] != 'X') continue;
                    count += Search1(grid, i, j);
                }
                else if (problem == 1)
                {
                    if (grid[i][j] != 'A') continue;
                    count += Search2(grid, i, j);
                }
                else
                {
                    Console.WriteLine("Problem not defined");
                    return;
                }
            }

        }
        Console.WriteLine("Number of occurence of XMAS: {0}", count);
    }

    private static int Search1(char[][] grid, int x, int y)
    {
        int count = 0;
        int[,] directions = new int[,] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 },
                                         { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } };
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];

            count += SearchDirection(grid, x, y, dx, dy, "XMAS");
        }
        return count;
    }

    private static int Search2(char[][] grid, int x, int y)
    {
        String refString = "MAS";
        bool diagonal1 = (SearchDirection(grid, x - 1, y - 1, 1, 1, refString) == 1) |
                         (SearchDirection(grid, x + 1, y + 1, -1, -1, refString) == 1);
        if (!diagonal1) return 0;
        bool diagonal2 = (SearchDirection(grid, x - 1, y + 1, 1, -1, refString) == 1) |
                    (SearchDirection(grid, x + 1, y - 1, -1, 1, refString) == 1);
        if (!diagonal2) return 0;

        return 1;
    }

    private static int SearchDirection(char[][] grid, int x, int y, int dx, int dy, String refWord)
    {
        for (int i = 0; i < refWord.Length; i++)
        {
            int nX = x + i * dx;
            int nY = y + i * dy;

            if (nX < 0 | nX >= grid.Length) return 0;
            if (nY < 0 | nY >= grid[0].Length) return 0;

            char c = grid[nX][nY];
            if (c != refWord[i]) return 0;
        }

        return 1;
    }
}
