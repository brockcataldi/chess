class Display
{
	public static void VerticalPadding(int lines)
	{
		for (int i = 0; i < lines; i++)
		{
			Console.WriteLine();
		}
	}

	public static void Title()
	{
		Console.WriteLine("            ('-. .-.   ('-.    .-')     .-')    ");
		Console.WriteLine("           ( OO )  / _(  OO)  ( OO ).  ( OO ).  ");
		Console.WriteLine("   .-----. ,--. ,--.(,------.(_)---\\_)(_)---\\_) ");
		Console.WriteLine("  '  .--./ |  | |  | |  .---'/    _ | /    _ |  ");
		Console.WriteLine("  |  |('-. |   .|  | |  |    \\  :` `. \\  :` `.  ");
		Console.WriteLine(" /_) |OO  )|       |(|  '--.  '..`''.) '..`''.) ");
		Console.WriteLine(" ||  |`-'| |  .-.  | |  .--' .-._)   \\.P._)   \\ ");
		Console.WriteLine("(_'  '--'\\ |  | |  | |  `---.\\       /\\       / ");
		Console.WriteLine("   `-----' `--' `--' `------' `-----'  `-----'  ");
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
	public static void Draw(Piece?[,] board, EntryResult entry, bool direction)
	{
		// Console.Clear();
		VerticalPadding(2);
		Console.WriteLine("    A  B  C  D  E  F  G  H ");

		for (int i = 0; i < 8; i++)
		{
			int y = direction ? 7 - i : i;
			bool yOffset = y % 2 == 0;

			Console.Write($" {y + 1} ");

			for (int j = 0; j < 8; j++)
			{
				bool jOffset = j % 2 == 0;
				ConsoleColor spaceColor = jOffset ? ConsoleColor.Green : ConsoleColor.White;

				if (!yOffset)
				{
					spaceColor = jOffset ? ConsoleColor.White : ConsoleColor.Green;
				}

				Piece? space = board[y, j];
				string piece = " ";
				ConsoleColor pieceColor = ConsoleColor.White;
				if (space != null)
				{
					piece = space.Symbol.ToString();
					pieceColor = space.Color ? ConsoleColor.Yellow : ConsoleColor.DarkBlue;
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
