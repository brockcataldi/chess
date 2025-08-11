/// <summary>
/// The game screen
/// </summary>
class GameScreen : IScreen
{
	/// <summary>
	/// The interal board state
	/// </summary>
	Piece?[,] Board { get; init; }

	/// <summary>
	/// Builds the board
	/// </summary>
	public GameScreen()
	{
		this.Board = new Piece?[8, 8];

		for (int i = 0; i < 8; i++)
		{
			this.Board[1, i] = new Pawn(true, 1, i);
			this.Board[6, i] = new Pawn(false, 6, i);
		}

		this.Board[0, 0] = new Rook(true, 0, 0);
		this.Board[0, 7] = new Rook(true, 0, 7);
		this.Board[7, 0] = new Rook(false, 7, 0);
		this.Board[7, 7] = new Rook(false, 7, 7);

		this.Board[0, 1] = new Knight(true, 0, 1);
		this.Board[0, 6] = new Knight(true, 0, 6);
		this.Board[7, 1] = new Knight(false, 7, 1);
		this.Board[7, 6] = new Knight(false, 7, 6);

		this.Board[0, 2] = new Bishop(true, 0, 2);
		this.Board[0, 5] = new Bishop(true, 0, 5);
		this.Board[7, 2] = new Bishop(false, 7, 2);
		this.Board[7, 5] = new Bishop(false, 7, 5);

		this.Board[0, 3] = new Queen(true, 0, 3);
		this.Board[7, 3] = new Queen(false, 7, 3);

		this.Board[0, 4] = new King(true, 0, 4);
		this.Board[7, 4] = new King(false, 7, 4);
	}

	/// <summary>
	/// Creates a new piece based on a character, either 'b', 'n', 'q', 'r'.
	/// 
	/// Meant to use for promoting pawns.
	/// </summary>
	/// <param name="piece">The piece to create</param>
	/// <param name="color">The color of the piece</param>
	/// <param name="rank">The rank of the piece</param>
	/// <param name="file">The file of the piece</param>
	/// <returns>Piece or null</returns>
	public static Piece? CreatePieceByChar(char piece, bool color, int rank, int file)
	{
		return piece switch
		{
			'b' => new Bishop(color, rank, file),
			'n' => new Knight(color, rank, file),
			'q' => new Queen(color, rank, file),
			'r' => new Rook(color, rank, file),
			_ => null,
		};
	}

	/// <summary>
	/// Runs a standard move.
	/// </summary>
	/// <param name="move"></param>
	/// <returns>EntryResult depending on what happened.</returns>
	public EntryResult Move(CommandParserResultMove move)
	{
		Piece? space = this.Board[move.Start.Rank, move.Start.File];

		if (space == null)
		{
			return new EntryResultError("Invalid Space.");
		}

		switch (space.CanMove(move.End, this.Board))
		{
			case CanMoveResultValid:
				this.Board[move.Start.Rank, move.Start.File] = null;
				this.Board[move.End.Rank, move.End.File] = space.Move(move.End);
				return new EntryResultValid();

			case CanMoveResultEnPassant enPassant:
				this.Board[move.Start.Rank, move.Start.File] = null;
				this.Board[enPassant.Position.Rank, enPassant.Position.File] = null;
				this.Board[move.End.Rank, move.End.File] = space.Move(move.End);
				return new EntryResultValid();

			case CanMoveResultPromote:
				return new EntryResultError("You need to add a piece character to promote.");

			case CanMoveResultError error:
				return new EntryResultError(error.Message);

			default:
				return new EntryResultError("Invalid Move.");
		}
	}

	/// <summary>
	/// Runs a promotion move.
	/// </summary>
	/// <param name="move"></param>
	/// <returns>EntryResult depending on what happened.</returns>
	public EntryResult Promote(CommandParserResultPromotion move)
	{
		Piece? space = this.Board[move.Start.Rank, move.Start.File];

		if (space == null)
		{
			return new EntryResultError("Invalid Space.");
		}

		switch (space.CanMove(move.End, this.Board))
		{
			case CanMoveResultPromote:
				Console.WriteLine("Promote");
				this.Board[move.Start.Rank, move.Start.File] = null;
				this.Board[move.End.Rank, move.End.File] = CreatePieceByChar(move.Promotion, space.Color, move.End.Rank, move.End.File);
				return new EntryResultValid();

			case CanMoveResultError error:
				return new EntryResultError(error.Message);
			case CanMoveResultEnPassant:
			case CanMoveResultValid:
			default:
				return new EntryResultError("Invalid Move.");
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="piece"></param>
	/// <returns></returns>
	public EntryResult Show(CommandParserResultShow piece)
	{
		Piece? space = this.Board[piece.Position.Rank, piece.Position.File];

		if (space == null)
		{
			return new EntryResultError("Invalid Space.");
		}

		return new EntryResultShow(space.GetAvailableMoves(this.Board));
	}

	/// <summary>
	/// Rendering the game loop.
	/// </summary>
	/// <param name="game"></param>
	public void Render(Game game)
	{
		bool running = false;
		EntryResult previousEntry = new EntryResultValid();

		while (!running)
		{
			Display.Draw(
				this.Board,
				previousEntry,
				true
			);
			Console.Write("Enter Move or Command: ");
			string? response = Console.ReadLine();

			if (response == null || response == "")
			{
				previousEntry = new EntryResultError("Try Again");
				continue;
			}

			CommandParserResult result = CommandParser.Parse(response);

			previousEntry = result switch
			{
				CommandParserResultMove move => this.Move(move),
				CommandParserResultPromotion move => this.Promote(move),
				CommandParserResultShow piece => this.Show(piece),
				CommandParserResultError error => new EntryResultError(error.Message),
				_ => new EntryResultError("Try Again")
			};
		}
	}
}
