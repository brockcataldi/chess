/// <summary>
/// Bishop Piece
/// </summary>
/// <param name="color">Color of the Bishop</param>
/// <param name="rank">Rank of the Bishop</param>
/// <param name="file">File of the Bishop</param>
class Bishop(bool color, int rank, int file) : Piece('B', color, rank, file)
{
    /// <summary>
    /// Whether or not the Bishop can move to a space
    /// </summary>
    /// <param name="to">The position to move to</param>
    /// <param name="board">The current board</param>
    /// <returns>Whether or not a piece can move, and why not.</returns>
    public override CanMoveResult CanMove(Position to, Piece?[,] board)
    {
        int rankDistance = to.Rank - Rank;
        int fileDistance = to.File - File;

        int rankDirection = Math.Sign(rankDistance);
        int fileDirection = Math.Sign(fileDistance);

        rankDistance = Math.Abs(rankDistance);

        if (rankDistance == Math.Abs(fileDistance))
        {
            for (int i = 1; i < rankDistance; i++)
            {
                if (board[Rank + (i * rankDirection),
                          File + (i * fileDirection)] != null)
                {
                    return new CanMoveResultError("There's a piece blocBishop");
                }
            }

            return CheckSpace(to, board);
        }

        return new CanMoveResultError("Invalid Move");
    }
}