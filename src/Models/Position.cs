public class Position(int rank, int file)
{
    /// <summary>
    /// The Rank of the Position (y)
    /// </summary>
    public int Rank { get; } = rank;

    /// <summary>
    /// The File of the Position (x)
    /// </summary>
    public int File { get; } = file;

    /// <summary>
    /// Converts the position (rank and file) to standard chess algebraic notation.
    /// Example: (rank: 0, file: 0) becomes "a1".
    /// </summary>
    public string GetNotation()
    {
        if (Rank > 7 || Rank < 0)
        {
            throw new Exception("Invalid Position");
        }

        if (File > 7 || File < 0)
        {
            throw new Exception("Invalid File");
        }

        return $"{Constants.FILES[File]}{Rank + 1}";
    }
}