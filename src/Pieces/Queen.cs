class Queen(bool color, int rank, int file) : Piece('Q', color, new int[8, 2] {
		{1, 0},
		{0, 1},
		{-1, 0},
		{0, -1},
		{1, 1},
		{1, -1},
		{-1, 1},
		{-1, -1}
	}, rank, file)
{

	public override List<Position> GetAvailableMoves(Piece?[,] board)
	{
		List<Position> moves = [];
		bool[] stopped = [false, false, false, false];

		for (int i = 1; i < 8; i++)
		{
			if (Utilities.AllTrue(stopped))
			{
				return moves;
			}

			for (int j = 0; j < 4; j++)
			{
				if (stopped[j] == false)
				{
					int rank = Position.Rank + (Vectors[j, 0] * i);
					int file = Position.File + (Vectors[j, 1] * i);

					if (InBounds(rank) && InBounds(file))
					{
						Piece? space = board[rank, file];

						if (space == null)
						{
							moves.Add(new Position(rank, file));
							continue;
						}

						if (space.Color != Color)
						{
							moves.Add(new Position(rank, file));
						}

						stopped[j] = true;
					}
					else
					{
						stopped[j] = true;
					}
				}
			}
		}

		return moves;
	}
}
