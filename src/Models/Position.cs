public class Position(int rank, int file) : IEquatable<Position>
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
	/// Allows .Contains and other functions to work with comparing positions
	/// </summary>
	/// <param name="other">The other position to compare</param>
	/// <returns>Whether other equals position</returns>
	public bool Equals(Position? other)
	{
		if (other == null)
		{
			return false;
		}

		return Rank == other.Rank && File == other.File;
	}

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
