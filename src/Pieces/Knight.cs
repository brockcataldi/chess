/// <summary>
/// Knight Piece
/// </summary>
/// <param name="color">Color of the Knight</param>
/// <param name="rank">Rank of the Knight</param>
/// <param name="file">File of the Knight</param>
class Knight(bool color, int rank, int file) : Piece('N', color, rank, file)
{
    /// <summary>
    /// Whether or not the knight can move to a the space
    /// </summary>
    /// <param name="to">The position to move to</param>
    /// <param name="board">The current board</param>
    /// <returns>Whether or not a piece can move, and why not.</returns>
    public override CanMoveResult CanMove(Position to, Piece?[,] board)
    {
        int rankDistance = Math.Abs(to.Rank - Rank);
        int fileDistance = Math.Abs(to.File - File);

        if ((rankDistance == 2 && fileDistance == 1)
        || (fileDistance == 2 && rankDistance == 1))
        {
            return CheckSpace(to, board);
        }

        return new CanMoveResultError("Invalid Move");
    }
}