class King(bool color, int rank, int file) : Piece('K', color, new int[1, 2] { { 1, 0 } }, rank, file)
{

	public override CanMoveResult CanMove(Position to, Piece?[,] board)
	{
		int rankDistance = Math.Abs(to.Rank - Position.Rank);
		int fileDistance = Math.Abs(to.File - Position.File);

		if ((rankDistance == 1 || rankDistance == 0)
		&& (fileDistance == 1 || fileDistance == 0))
		{
			return CheckSpace(to, board);
		}
		return new CanMoveResultError("Invalid Move");
	}

	public override List<Position> GetAvailableMoves(Piece?[,] board)
	{
		throw new NotImplementedException();
	}
}
