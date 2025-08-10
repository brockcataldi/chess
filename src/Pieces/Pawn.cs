/// <summary>
/// Pawn Piece
/// </summary>
/// <param name="color">The Color of the Pawn</param>
/// <param name="rank">Rank of the Pawn</param>
/// <param name="file">File of the Pawn</param>
class Pawn(bool color, int rank, int file) : Piece('P', color, new int[1, 2] { { 1, 0 } }, rank, file)
{
	/// <summary>
	/// Whether on not the pawn is on the starting position
	/// </summary>
	public bool StartingPosition { get; set; } = true;

	/// <summary>
	/// Whether or not the pawn can be enpassantable.
	/// </summary>
	public bool EnPassant { get; set; } = false;

	/// <summary>
	/// Whether or not the Pawn can move to a space
	/// </summary>
	/// <param name="to">The position to move to</param>
	/// <param name="board">The current board</param>
	/// <returns>Whether or not a piece can move, and why not.</returns>
	public override CanMoveResult CanMove(Position to, Piece?[,] board)
	{
		int rankDistance = Color ? to.Rank - Position.Rank : Position.Rank - to.Rank;
		int fileDistance = Math.Abs(to.File - Position.File);
		int direction = Color ? 1 : -1;

		if (StartingPosition && rankDistance == 2)
		{
			if (board[Position.Rank + direction, Position.File] == null
			&& board[to.Rank, Position.File] == null)
			{
				return new CanMoveResultValid();
			}
		}

		if (rankDistance == 1)
		{
			if (fileDistance == 0)
			{
				bool shouldPromote = Color ? (Position.Rank + 1) == 7 : (Position.Rank - 1) == 0;

				if (shouldPromote)
				{
					return new CanMoveResultPromote();
				}

				if (board[to.Rank, Position.File] == null)
				{
					return new CanMoveResultValid();
				}
			}

			if (fileDistance == 1)
			{
				Piece? target = board[to.Rank, to.File];
				if (target != null && target.Color != Color)
				{
					return new CanMoveResultValid();
				}

				Piece? enPassant = board[Position.Rank, to.File];
				if (enPassant != null)
				{
					if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != Color)
					{
						return new CanMoveResultEnPassant(pawn.Position);
					}
				}
			}
		}

		return new CanMoveResultError("Invalid Move");
	}

	public override List<Position> GetAvailableMoves(Piece?[,] board)
	{


		throw new NotImplementedException();
	}

	/// <summary>
	/// Updating internal mechanisms of the piece. 
	/// </summary>
	/// <param name="to">The move location.</param>
	/// <param name="board">The state of the board.</param>
	/// <returns>Updated Pawn</returns>
	public override Piece Move(Position to)
	{
		int distance = Color ? to.Rank - Position.Rank : Position.Rank - to.Rank;

		if (StartingPosition == true && distance == 2)
		{
			EnPassant = true;
		}
		else
		{
			EnPassant = false;
		}

		StartingPosition = false;
		Position = to;

		return this;
	}
}
