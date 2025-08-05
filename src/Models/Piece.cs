abstract class Piece(char symbol, bool color, int rank, int file)
{
    public int Rank { get; set; } = rank;
    public int File { get; set; } = file;
    public char Symbol { get; init; } = symbol;
    public bool Color { get; init; } = color;

    public CanMoveResult CheckSquare(Position to, Piece?[,] board)
    {
        Piece? space = board[to.Rank, to.File];

        if (space == null)
        {
            return new CanMoveResultValid();
        }

        return (space.Color != Color) ?
            new CanMoveResultValid() :
            new CanMoveResultError("Your piece is there");
    }

    public abstract CanMoveResult CanMove(Position to, Piece?[,] board);   
    public abstract Piece Move(Position to);   
}