class Knight(bool color, int rank, int file) : Piece('N', color, new int[8, 2] {
		{2, 1},
		{1, 2},
		{-2, 1},
		{-1, 2},
		{2, -1},
		{1, -2},
		{-2, -1},
		{-1, -2}
	}, rank, file)
{
	public override List<Position> GetAvailableMoves(Piece?[,] board)
	{
		List<Position> moves = [];

		for (int i = 0; i < 8; i++)
		{
			int rank = Vectors[i, 0] + Position.Rank;
			int file = Vectors[i, 1] + Position.File;

			if (InBounds(rank) && InBounds(file))
			{
				Piece? space = board[rank, file];

				if (space == null || space.Color != Color)
				{
					moves.Add(new Position(rank, file));
					continue;
				}
			}
		}

		return moves;
	}
}
