class Display
{
    public static void VerticalPadding(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            Console.WriteLine();
        }
    }

    public static void DrawSpace(string piece, ConsoleColor pieceColor, ConsoleColor spaceColor)
    {
        Console.ForegroundColor = spaceColor;
        Console.Write("[");
        Console.ForegroundColor = pieceColor;
        Console.Write(piece);
        Console.ForegroundColor = spaceColor;
        Console.Write("]");
        Console.ResetColor();
    }
    public static void Draw(Piece?[][] board, EntryResult entry)
    {
        // Console.Clear();
        VerticalPadding(2);
        Console.WriteLine("    A  B  C  D  E  F  G  H ");

        for (int i = 0; i < 8; i++)
        {
            bool iOffset = i % 2 == 0;

            Console.Write($" {i + 1} ");
            
            for (int j = 0; j < 8; j++)
            {
                bool jOffset = j % 2 == 0;
                ConsoleColor spaceColor = (jOffset) ? ConsoleColor.Green : ConsoleColor.White;

                if (!iOffset)
                {
                    spaceColor = (jOffset) ? ConsoleColor.White : ConsoleColor.Green;
                }

                Piece? space = board[i][j];
                string piece = " ";
                ConsoleColor pieceColor = ConsoleColor.White;
                if (space != null)
                {
                    piece = space.Symbol.ToString();
                    pieceColor = (space.Color) ? ConsoleColor.Yellow : ConsoleColor.DarkBlue;
                }

                DrawSpace(piece, pieceColor, spaceColor);
            }

            Console.Write(Environment.NewLine);
        }

        if (entry is EntryResultError error)
        {
            VerticalPadding(1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error.Message);
            Console.ResetColor();
        }
        else
        {
            VerticalPadding(2);
        }
    }
}