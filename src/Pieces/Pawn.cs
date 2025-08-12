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

	public List<Move> GetAttackMoves(int rank, int file, Piece?[,] board)
	{
		List<Move> moves = [];

		if (InBounds(file))
		{
			Piece? piece = board[rank, file];
			if (piece != null && piece.Color != this.Color)
			{
				moves.Add(new MoveStandard(piece.Position));
			}

			Piece? enPassant = board[this.Position.Rank, file];
			if (enPassant != null)
			{
				if (enPassant is Pawn pawn && pawn.EnPassant && pawn.Color != this.Color)
				{
					moves.Add(
						new MoveEnPassant(
							new Position(rank, file),
							pawn.Position
						)
					);
				}
			}
		}
		return moves;
	}

	public override List<Move> GetAvailableMoves(Piece?[,] board)
	{
		List<Move> moves = [];
		int direction = this.Color ? 1 : -1;
		int promote = this.Color ? 7 : 0;

		int forward = this.Position.Rank + direction;
		int left = this.Position.File - 1;
		int right = this.Position.File + 1;
		
		if (board[forward, this.Position.File] == null)
		{

			moves.Add(forward == promote ?
				new MovePromote(
					new Position(forward, this.Position.File)
				) :
				new MoveStandard(
					new Position(forward, this.Position.File)
				)
			);

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

		moves.AddRange(this.GetAttackMoves(forward, left, board));
		moves.AddRange(this.GetAttackMoves(forward, right, board));

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
