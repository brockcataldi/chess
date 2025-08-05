public class Position(int rank, int file)
{
    public int Rank { get; } = rank;
    public int File { get; } = file;

    /// <summary>
    /// Converts the position (rank and file) to standard chess algebraic notation.
    /// Example: (rank: 0, file: 0) becomes "a1".
    /// </summary>
    public string GetNotation()
    {
        return $"{Constants.FILES[File]}{Rank + 1}";
    }
}