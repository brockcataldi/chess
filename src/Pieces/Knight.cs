class Knight(bool color, int rank, int file) : 
	Piece(
		'N',
		new int[8, 2] {
			{2, 1},
			{1, 2},
			{-2, 1},
			{-1, 2},
			{2, -1},
			{1, -2},
			{-2, -1},
			{-1, -2}
		},
		0, // This is overriden
		color,
		rank,
		file
	)
{
	/// <summary>
	/// Since Knight isn't a "draggable" piece,  this had to be overridden.
	/// </summary>
	/// <param name="board">The board</param>
	/// <returns>The available moves</returns>
	public override List<Position> GetAvailableMoves(Piece?[,] board)
	{
		List<Position> moves = [];

		for (int i = 0; i < 8; i++)
		{
			int rank = this.Vectors[i, 0] + this.Position.Rank;
			int file = this.Vectors[i, 1] + this.Position.File;

			if (InBounds(rank) && InBounds(file))
			{
				Piece? space = board[rank, file];

				if (space == null || space.Color != this.Color)
				{
					moves.Add(new Position(rank, file));
					continue;
				}
			}
		}

		return moves;
	}
}
