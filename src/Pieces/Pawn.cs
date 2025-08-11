class Pawn(bool color, int rank, int file) : 
	Piece(
		'P',
		new int[1, 2] {
			{ 1, 0 }
		},
		0,
		color,
		rank,
		file
	)
{
	public bool StartingPosition { get; set; } = true;

	public bool EnPassant { get; set; } = false;

	public override List<Position> GetAvailableMoves(Piece?[,] board)
	{
		List<Position> moves = [];
		int direction = this.Color ? 1 : -1;

		int forward = this.Position.Rank + direction;
		int leftAtt = this.Position.File - 1;
		int rightAtt = this.Position.File + 1;

		if (board[forward, this.Position.File] == null)
		{
			moves.Add(new Position(forward, this.Position.File));

			int jump = this.Position.Rank + (direction * 2);
			if (this.StartingPosition && board[jump, this.Position.File] == null)
			{
				moves.Add(new Position(jump, this.Position.File));
			}
		}

		if (InBounds(leftAtt))
		{
			Piece? left = board[forward, leftAtt];
			if (left != null && left.Color != this.Color)
			{
				moves.Add(left.Position);
			}

			Piece? enPassant = board[this.Position.Rank, leftAtt];
			if (enPassant != null)
			{
				if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != this.Color)
				{
					moves.Add(new Position(forward, leftAtt));
				}
			}
		}

		if (InBounds(rightAtt))
		{
			Piece? right = board[forward, rightAtt];
			if (right != null && right.Color != this.Color)
			{
				moves.Add(right.Position);
			}
			
			Piece? enPassant = board[this.Position.Rank, rightAtt];
			if (enPassant != null)
			{
				if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != this.Color)
				{
					moves.Add(new Position(forward, rightAtt));
				}
			}
		}

		return moves;
	}

	public override Piece Move(Position to)
	{
		int distance = Math.Abs(this.Position.Rank - to.Rank);

		if (this.StartingPosition == true && distance == 2)
		{
			this.EnPassant = true;
		}
		else
		{
			this.EnPassant = false;
		}

		this.StartingPosition = false;
		return base.Move(to);
	}
}
