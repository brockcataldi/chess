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

	public override List<Move> GetAvailableMoves(Piece?[,] board)
	{
		List<Move> moves = [];
		int direction = this.Color ? 1 : -1;
		int promote = this.Color ? 7 : 0;

		int forward = this.Position.Rank + direction;
		int leftAtt = this.Position.File - 1;
		int rightAtt = this.Position.File + 1;

		if (board[forward, this.Position.File] == null)
		{

			if (forward == promote)
			{
				moves.Add(
					new MovePromote(
						new Position(forward, this.Position.File)
					)
				);	
			}
			else
			{
				moves.Add(
					new MoveStandard(
						new Position(forward, this.Position.File)
					)
				);				
			}

			int jump = this.Position.Rank + (direction * 2);
			if (this.StartingPosition && board[jump, this.Position.File] == null)
			{
				moves.Add(
					new MoveStandard(
						new Position(jump, this.Position.File)
					)
				);
			}
		}

		if (InBounds(leftAtt))
		{
			Piece? left = board[forward, leftAtt];
			if (left != null && left.Color != this.Color)
			{
				moves.Add(new MoveStandard(left.Position));
			}

			Piece? enPassant = board[this.Position.Rank, leftAtt];
			if (enPassant != null)
			{
				if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != this.Color)
				{
					moves.Add(
						new MoveEnPassant(
							new Position(forward, leftAtt),
							pawn.Position
						)
					);
				}
			}

		}

		if (InBounds(rightAtt))
		{
			Piece? right = board[forward, rightAtt];
			if (right != null && right.Color != this.Color)
			{
				moves.Add(new MoveStandard(right.Position));
			}
			
			Piece? enPassant = board[this.Position.Rank, rightAtt];
			if (enPassant != null)
			{
				if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != this.Color)
				{
					moves.Add(
						new MoveEnPassant(
							new Position(forward, leftAtt),
							pawn.Position
						)
					);
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
