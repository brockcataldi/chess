class Queen(bool color, int rank, int file) : Piece('Q', color, rank, file)
{
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

            return CheckSquare(to, board);
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

            return CheckSquare(to, board);
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

            return CheckSquare(to, board);
        }

        return new CanMoveResultError("Invalid Move");
    }

    public override Piece Move(Position to)
    {
        Rank = to.Rank;
        File = to.File;
        return this;
    }
}