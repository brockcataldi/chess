/// <summary>
/// King Piece
/// </summary>
/// <param name="color">Color of the King</param>
/// <param name="rank">Rank of the King</param>
/// <param name="file">File of the King</param>
class King(bool color, int rank, int file) : Piece('K', color, rank, file)
{
    /// <summary>
    /// Whether or not the King can move to a space
    /// </summary>
    /// <param name="to">The position to move to</param>
    /// <param name="board">The current board</param>
    /// <returns>Whether or not a piece can move, and why not.</returns>
    public override CanMoveResult CanMove(Position to, Piece?[,] board)
    {
        int rankDistance = Math.Abs(to.Rank - Rank);
        int fileDistance = Math.Abs(to.File - File);

        if ((rankDistance == 1 || rankDistance == 0)
        && (fileDistance == 1 || fileDistance == 0))
        {
            return CheckSpace(to, board);
        }
        return new CanMoveResultError("Invalid Move");
    }
}