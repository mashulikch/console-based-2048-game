using System;

namespace Game2048
{
    class Program
    {
        const int Size = 4;
        static int[,] board = new int[Size, Size];
        static Random random = new Random();
        static int score = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            AddRandomTile();
            AddRandomTile();

            while (true)
            {
                Console.Clear();
                PrintBoard();

                if (HasWon())
                {
                    Console.WriteLine("You've collected 2048! Victory!");
                    break;
                }

                if (!CanMove())
                {
                    Console.WriteLine("There are no more moves. The game is over.");
                    break;
                }

                Console.WriteLine("Control: W/A/S/D or up/left/down/right. Q or Enter — exit.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                bool moved = false;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        moved = MoveUp();
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:    
                        moved = MoveDown();
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:    
                        moved = MoveLeft();
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:    
                        moved = MoveRight();
                        break;
                    case ConsoleKey.Q:
                    case ConsoleKey.Enter:    
                        return;
                }

                if (moved)
                {
                    AddRandomTile();
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        #region Motion logic

        static bool MoveLeft()
        {
            bool moved = false;
            for (int row = 0; row < Size; row++)
            {
                int[] line = new int[Size];
                for (int col = 0; col < Size; col++)
                    line[col] = board[row, col];

                bool lineMoved = ProcessLine(line);

                for (int col = 0; col < Size; col++)
                    board[row, col] = line[col];

                if (lineMoved) moved = true;
            }
            return moved;
        }

        static bool MoveRight()
        {
            bool moved = false;
            for (int row = 0; row < Size; row++)
            {
                int[] line = new int[Size];
                for (int col = 0; col < Size; col++)
                    line[col] = board[row, Size - 1 - col];

                bool lineMoved = ProcessLine(line);

                for (int col = 0; col < Size; col++)
                    board[row, Size - 1 - col] = line[col];

                if (lineMoved) moved = true;
            }
            return moved;
        }

        static bool MoveUp()
        {
            bool moved = false;
            for (int col = 0; col < Size; col++)
            {
                int[] line = new int[Size];
                for (int row = 0; row < Size; row++)
                    line[row] = board[row, col];

                bool lineMoved = ProcessLine(line);

                for (int row = 0; row < Size; row++)
                    board[row, col] = line[row];

                if (lineMoved) moved = true;
            }
            return moved;
        }

        static bool MoveDown()
        {
            bool moved = false;
            for (int col = 0; col < Size; col++)
            {
                int[] line = new int[Size];
                for (int row = 0; row < Size; row++)
                    line[row] = board[Size - 1 - row, col];

                bool lineMoved = ProcessLine(line);

                for (int row = 0; row < Size; row++)
                    board[Size - 1 - row, col] = line[row];

                if (lineMoved) moved = true;
            }
            return moved;
        }

        // Single line processing: compression -> merge -> compression
        static bool ProcessLine(int[] line)
        {
            bool moved = false;

            // Move all non-zero points to the left
            int[] compressed = new int[Size];
            int index = 0;
            for (int i = 0; i < Size; i++)
            {
                if (line[i] != 0)
                {
                    compressed[index] = line[i];
                    if (i != index) moved = true;
                    index++;
                }
            }

            // Merge pairs of identical
            for (int i = 0; i < Size - 1; i++)
            {
                if (compressed[i] != 0 && compressed[i] == compressed[i + 1])
                {
                    compressed[i] *= 2;
                    score += compressed[i];
                    compressed[i + 1] = 0;
                    moved = true;
                }
            }

            // Slide again after merging
            int[] result = new int[Size];
            index = 0;
            for (int i = 0; i < Size; i++)
            {
                if (compressed[i] != 0)
                {
                    result[index] = compressed[i];
                    index++;
                }
            }

            // Write it back to the line
            for (int i = 0; i < Size; i++)
            {
                if (line[i] != result[i]) moved = true;
                line[i] = result[i];
            }

            return moved;
        }

        #endregion

        #region Auxiliary methods

        static void AddRandomTile()
        {
            // Collect a list of empty cells
            var emptyCells = new List<(int r, int c)>();
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (board[r, c] == 0)
                        emptyCells.Add((r, c));
                }
            }

            if (emptyCells.Count == 0)
                return;

            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            board[row, col] = random.NextDouble() < 0.9 ? 2 : 4;
        }

        static void PrintBoard()
        {
            Console.WriteLine("Score: " + score);
            Console.WriteLine(new string('-', Size * 7));

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    int value = board[r, c];
                    string text = value == 0 ? "." : value.ToString();
                    Console.Write($"{text,5} ");
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', Size * 7));
            }
        }

        static bool HasWon()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    if (board[r, c] == 2048)
                        return true;
            return false;
        }

        static bool CanMove()
        {
            // Is there an empty cell?
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    if (board[r, c] == 0)
                        return true;

            // Is it possible to merge neighbors?
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    int current = board[r, c];
                    // right
                    if (c + 1 < Size && board[r, c + 1] == current)
                        return true;
                    // down
                    if (r + 1 < Size && board[r + 1, c] == current)
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}
