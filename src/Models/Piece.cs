abstract class Piece(char symbol, bool color, int rank, int file)
{
    public int Rank { get; set; } = rank;
    public int File { get; set; } = file;
    public char Symbol { get; init; } = symbol;
    public bool Color { get; init; } = color;

    public abstract bool CanMove(Position to, Piece?[][] board);   
    public abstract Piece Move(Position to, Piece?[][] board);   


}