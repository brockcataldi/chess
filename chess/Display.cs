class Display
{
    public static void VerticalPadding(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            Console.WriteLine();
        }
    }
    public static void Draw(Piece?[][] board)
    {
        Console.Clear();
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


                Console.ForegroundColor = spaceColor;
                Console.Write("[");
                Console.ForegroundColor = pieceColor;
                Console.Write(piece);
                Console.ForegroundColor = spaceColor;
                Console.Write("]");
                Console.ResetColor();
            }

            Console.Write(Environment.NewLine);
        }
        VerticalPadding(2);
    }
}