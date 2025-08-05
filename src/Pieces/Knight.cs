/// <summary>
/// 
/// </summary>
/// <param name="color"></param>
/// <param name="rank"></param>
/// <param name="file"></param>
class Knight(bool color, int rank, int file) : Piece('N', color, rank, file)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    /// <param name="board"></param>
    /// <returns></returns>
    public override CanMoveResult CanMove(Position to, Piece?[,] board)
    {

        int rankDistance = Math.Abs(to.Rank - Rank);
        int fileDistance = Math.Abs(to.File - File);

        if ((rankDistance == 2 && fileDistance == 1)
        || (fileDistance == 2 && rankDistance == 1))
        {
            Piece? space = board[to.Rank, to.File];

            if (space == null)
            {
                return new CanMoveResultValid();
            }

            return (space.Color != Color) ? new CanMoveResultValid()
            : new CanMoveResultError("Your piece is there");
        }

        return new CanMoveResultError("Invalid Move");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public override Piece Move(Position to)
    {
        Rank = to.Rank;
        File = to.File;
        return this;
    }
}