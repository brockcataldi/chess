/// <summary>
/// Position object used across the whole game.
/// </summary>
/// <param name="rank">The rank of the piece</param>
/// <param name="file">The file of the piece</param>
record Position(int Rank, int File){
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

abstract record CommandParserResult;
record CommandParserResultMove(Position Start, Position End) : CommandParserResult;
record CommandParserResultShow(Position Position): CommandParserResult;
record CommandParserResultPromotion(Position Start, Position End, char Promotion) : CommandParserResult;
record CommandParserResultError(string Message) : CommandParserResult;

abstract record EntryResult;
record EntryResultValid : EntryResult;
record EntryResultShow(List<Move> Moves): EntryResult;
record EntryResultError(string Message) : EntryResult;

abstract record Move(Position Position)
{
    public virtual bool Equals(Move? other) {
        return other is not null && this.Position.Equals(other.Position);
    }

    public override int GetHashCode()
    {
        return this.Position.GetHashCode();
    }
};

record MoveStandard(Position Position) : Move(Position);
record MovePromote(Position Position) : Move(Position);
record MoveEnPassant(Position Position, Position Target) : Move(Position);
record MoveError(Position Position, string Message) : Move(Position);


