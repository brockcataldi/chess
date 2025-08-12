/// <summary>
/// Piece Base Class
/// </summary>
/// <param name="symbol">A Character representation of the piece</param>
/// <param name="vectors">The vectors a piece can move</param>
/// <param name="max">The max tiles a piece can move</param>
/// <param name="color">The Color of the Piece</param>
/// <param name="rank">The Rank of the Piece</param>
/// <param name="file">The file of the Piece</param>
abstract class Piece(char symbol, int[,] vectors, int max, bool color, int rank, int file)
{
	/// <summary>
	/// "Draggable" Vectors for each piece. Expecting [n, 2] depth.
	/// </summary>
	public int[,] Vectors { get; init; } = vectors;

	/// <summary>
	/// The maximum number of pieces a draggable piece can move.
	/// </summary>
	public int Max { get; init; } = max;

	/// <summary>
	/// Position of the Piece
	/// </summary>
	public Position Position { get; set; } = new Position(rank, file);

	/// <summary>
	/// The Symbol of the Piece.
	/// </summary>
	public char Symbol { get; init; } = symbol;

	/// <summary>
	/// The Color of the Piece.
	/// </summary>
	public bool Color { get; init; } = color;

	/// <summary>
	/// Determines if a value is inbound.
	/// </summary>
	/// <param name="value">the value to check</param>
	/// <returns>Whether or not the move in bounds</returns>
	public static bool InBounds(int value)
	{
		return value < 8 && value > -1;
	}

	/// <summary>
	/// Whether or not the Piece can move to a space.
	/// </summary>
	/// <param name="to">The position to move to</param>
	/// <param name="board">The current state of the board</param>
	/// <returns>The actual move</returns>
	public virtual Move CanMove(Position to, Piece?[,] board)
	{
		List<Move> moves = this.GetAvailableMoves(board);
		Move? move = moves.SingleOrDefault(m => m.Position == to);

		if (move == null)
		{
			return new MoveError(to, "Invalid Move.");
		}

		return move;
	}

	/// <summary>
	/// Returns an array of all of the piece's available moves.
	/// </summary>
	/// <param name="board">The current board.</param>
	/// <returns>All of the positions.</returns>
	public virtual List<Move> GetAvailableMoves(Piece?[,] board)
	{
		List<Move> moves = [];
		bool[] stopped = new bool[this.Vectors.GetLength(0)];

		for (int i = 1; i < this.Max; i++)
		{
			// I might need to come up with a more clever way to do this?
			if (Utilities.AllTrue(stopped))
			{
				return moves;
			}

			for (int j = 0; j < this.Vectors.GetLength(0); j++)
			{
				if (stopped[j] == false)
				{
					continue;
				}

				int rank = this.Position.Rank + (this.Vectors[j, 0] * i);
				int file = this.Position.File + (this.Vectors[j, 1] * i);

				if (!InBounds(rank) && !InBounds(file))
				{
					stopped[j] = true;
				}

				Piece? space = board[rank, file];

				if (space == null)
				{
					moves.Add(new MoveStandard(new Position(rank, file)));
					continue;
				}

				if (space.Color != this.Color)
				{
					moves.Add(new MoveStandard(new Position(rank, file)));
				}

				stopped[j] = true;
			}
		}

		return moves;
	}

	/// <summary>
	/// Move the piece internally
	/// </summary>
	/// <param name="to">The position to move to</param>
	/// <returns>The updated piece</returns>
	public virtual Piece Move(Position to)
	{
		this.Position = to;
		return this;
	}
}
