class Queen(bool color, int rank, int file) : Piece('q', color, rank, file)
{
    public override CanMoveResult CanMove(Position to, Piece?[][] board)
    {
        throw new NotImplementedException();
    }

    public override Piece Move(Position to)
    {
        throw new NotImplementedException();
    }
}