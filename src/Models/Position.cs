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

		return this.Rank == other.Rank && this.File == other.File;
	}

	/// <summary>
	/// Converts the position (rank and file) to standard chess algebraic notation.
	/// Example: (rank: 0, file: 0) becomes "a1".
	/// </summary>
	public string GetNotation()
	{
		if (this.Rank > 7 || this.Rank < 0)
		{
			throw new Exception("Invalid Position");
		}

		if (this.File > 7 || this.File < 0)
		{
			throw new Exception("Invalid File");
		}

		return $"{Constants.FILES[this.File]}{this.Rank + 1}";
	}
}
