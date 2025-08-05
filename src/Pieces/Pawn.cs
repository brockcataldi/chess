/// <summary>
/// The Pawn Piece
/// </summary>
/// <param name="color">The color of the piece</param>
/// <param name="rank">The rank position</param>
/// <param name="file">The file position</param>
class Pawn(bool color, int rank, int file) : Piece('P', color, rank, file)
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
    /// Whether or not the move is valid.
    /// </summary>
    /// <param name="to">Where the pawn is supposed to move to.</param>
    /// <param name="board">The current state of the board.</param>
    /// <returns>CanMoveResult</returns>
    public override CanMoveResult CanMove(Position to, Piece?[,] board)
    {
        int rankDistance = Color ? to.Rank - Rank : Rank - to.Rank;
        int fileDistance = Math.Abs(to.File - File);
        int direction = Color ? 1 : -1;

        if (StartingPosition && rankDistance == 2)
        {
            if (board[Rank + direction,File] == null
            && board[to.Rank, File] == null)
            {
                return new CanMoveResultValid();
            }
        }

        if (rankDistance == 1)
        {
            if (fileDistance == 0)
            {
                bool shouldPromote = Color ? (Rank + 1) == 7 : (Rank - 1) == 0;

                if (shouldPromote)
                {
                    return new CanMoveResultPromote();
                }
                
                if (board[to.Rank,File] == null)
                {
                    return new CanMoveResultValid();
                }
            }

            if (fileDistance == 1)
            {
                Piece? target = board[to.Rank,to.File];
                if (target != null && target.Color != Color)
                {
                    return new CanMoveResultValid();
                }

                Piece? enPassant = board[Rank,to.File]; 
                if (enPassant != null )
                {
                    if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != Color)
                    {
                        return new CanMoveResultEnPassant(new Position(pawn.Rank, pawn.File));
                    }
                }
            }
        }

        return new CanMoveResultError("Invalid Move");
    }

    /// <summary>
    /// Updating internal mechanisms of the piece. 
    /// </summary>
    /// <param name="to">The move location.</param>
    /// <param name="board">The state of the board.</param>
    /// <returns>Updated Pawn</returns>
    public override Piece Move(Position to)
    {
        int distance = Color ? to.Rank - Rank : Rank - to.Rank;

        if (StartingPosition == true && distance == 2)
        {
            EnPassant = true;
        }
        else
        {
            EnPassant = false;
        }

        StartingPosition = false;
        Rank = to.Rank;
        File = to.File;

        return this;
    }
}