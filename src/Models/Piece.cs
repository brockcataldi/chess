/// <summary>
/// Piece Base Class
/// </summary>
/// <param name="symbol">A Character representation of the piece</param>
/// <param name="color">The Color of the Piece</param>
/// <param name="rank">The Rank of the Piece</param>
/// <param name="file">The file of the Piece</param>
abstract class Piece(char symbol, bool color, int rank, int file)
{
    /// <summary>
    /// The Rank of the Piece.
    /// </summary>
    public int Rank { get; set; } = rank;

    /// <summary>
    /// The File of the Piece.
    /// </summary>
    public int File { get; set; } = file;

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

    public abstract CanMoveResult CanMove(Position to, Piece?[,] board);
    public virtual Piece Move(Position to)
    {
        Rank = to.Rank;
        File = to.File;
        return this;
    }
}