/// <summary>
/// Queen Piece
/// </summary>
/// <param name="color">Color of the Queen</param>
/// <param name="rank">Rank of the Queen</param>
/// <param name="file">File of the Queen</param>
class Queen(bool color, int rank, int file) : Piece('Q', color, rank, file)
{
    /// <summary>
    /// Whether or not the Queen can move to a space
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

        if (fileDistance == 0)
        {
            rankDistance = Math.Abs(rankDistance);

            for (int i = 1; i < rankDistance; i++)
            {
                if (board[Rank + (i * rankDirection), File] != null)
                {
                    return new CanMoveResultError("There's a piece blocking");
                }
            }

            return CheckSpace(to, board);
        }

        if (rankDistance == 0)
        {
            fileDistance = Math.Abs(fileDistance);

            for (int i = 1; i < fileDistance; i++)
            {
                if (board[Rank, File + (i * fileDirection)] != null)
                {
                    return new CanMoveResultError("There's a piece blocking");
                }
            }

            return CheckSpace(to, board);
        }

        if (rankDistance == Math.Abs(fileDistance))
        {
            for (int i = 1; i < rankDistance; i++)
            {
                if (board[Rank + (i * rankDirection),
                          File + (i * fileDirection)] != null)
                {
                    return new CanMoveResultError("There's a piece blocking");
                }
            }

            return CheckSpace(to, board);
        }

        return new CanMoveResultError("Invalid Move");
    }
}