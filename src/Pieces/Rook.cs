/// <summary>
/// Rook Piece
/// </summary>
/// <param name="color">Color of the Rook</param>
/// <param name="rank">Rank of the Rook</param>
/// <param name="file">File of the Rook</param>
class Rook(bool color, int rank, int file) : Piece('R', color, rank, file)
{
    /// <summary>
    /// Whether or not the Rook can move to a space
    /// </summary>
    /// <param name="to">The position to move to</param>
    /// <param name="board">The current board</param>
    /// <returns>Whether or not a piece can move, and why not.</returns>
    public override CanMoveResult CanMove(Position to, Piece?[,] board)
    {
        int rankDistance = to.Rank - Rank;
        int fileDistance = to.File - File;

        if (fileDistance == 0)
        {
            int rankDirection = Math.Sign(rankDistance);
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

            int fileDirection = Math.Sign(fileDistance);
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

        return new CanMoveResultError("Invalid Move");
    }
}