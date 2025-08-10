/// <summary>
/// Piece Base Class
/// </summary>
/// <param name="symbol">A Character representation of the piece</param>
/// <param name="color">The Color of the Piece</param>
/// <param name="rank">The Rank of the Piece</param>
/// <param name="file">The file of the Piece</param>
abstract class Piece(char symbol, bool color, int[,] vectors, int rank, int file)
{

	public int[,] Vectors { get; init; } = vectors;

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
	/// Determines the result of moving a piece to a space.
	/// </summary>
	/// <param name="to"></param>
	/// <param name="board"></param>
	/// <returns></returns>
	public CanMoveResult CheckSpace(Position to, Piece?[,] board)
	{
		Piece? space = board[to.Rank, to.File];

		if (space == null)
		{
			return new CanMoveResultValid();
		}

		return (space.Color != Color) ?
			new CanMoveResultValid() :
			new CanMoveResultError("Your piece is there");
	}

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
	/// <param name="to"></param>
	/// <param name="board"></param>
	/// <returns></returns>
	public virtual CanMoveResult CanMove(Position to, Piece?[,] board)
	{
		List<Position> moves = GetAvailableMoves(board);
		if (moves.Contains(to))
		{
			return new CanMoveResultValid();
		}

		return new CanMoveResultError("Invalid Move");
	}

	/// <summary>
	/// Returns an array of all of the piece's available moves.
	/// </summary>
	/// <param name="board">The current board.</param>
	/// <returns>All of the positions.</returns>
	public abstract List<Position> GetAvailableMoves(Piece?[,] board);

	/// <summary>
	/// Move the piece internally
	/// </summary>
	/// <param name="to">The position to move to</param>
	/// <returns>The updated piece</returns>
	public virtual Piece Move(Position to)
	{
		Position = to;
		return this;
	}
}
