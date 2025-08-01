class Pawn(bool color, int rank, int file) : Piece('p', color, rank, file)
{
    public bool StartingPosition { get; set; } = true;

    public bool EnPassant { get; set; } = false;

    public override bool CanMove(Position to, Piece?[][] board)
    {
        // attack positions include rank +1 file+1 or rank+1 file-1;

        int distance = Color ? to.Rank - Rank: Rank - to.Rank;

        if (StartingPosition && distance == 2)
        {
            return board[Color ? Rank + 1 : Rank - 1][File] == null
                && board[Color ? Rank + 2 : Rank - 2][File] == null;
        }

        if (distance == 1)
        {
            bool withinBounds = Color ? (Rank + 1) < 7 : (Rank - 1) > -1;

            if (withinBounds)
            {
                return board[Color ? Rank + 1 : Rank - 1][File] == null;
            }
        }

        return false;
    }

    public override Piece Move(Position to, Piece?[][] board)
    {
        int distance = Color ? to.Rank - Rank: Rank - to.Rank;

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