/// <summary>
/// 
/// </summary>
class GameScreen : IScreen
{
    /// <summary>
    /// 
    /// </summary>
    Piece?[][] Board { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public GameScreen()
    {
        this.Board = [
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [
                new Pawn(true, 1, 0),
                new Pawn(true, 1, 1),
                new Pawn(true, 1, 2),
                new Pawn(true, 1, 3),
                new Pawn(true, 1, 4),
                new Pawn(true, 1, 5),
                new Pawn(true, 1, 6),
                new Pawn(true, 1, 7)
            ],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [
                new Pawn(false, 6, 0),
                new Pawn(false, 6, 1),
                new Pawn(false, 6, 2),
                new Pawn(false, 6, 3),
                new Pawn(false, 6, 4),
                new Pawn(false, 6, 5),
                new Pawn(false, 6, 6),
                new Pawn(false, 6, 7)
            ],
            [.. Enumerable.Repeat<Piece?>(null, 8)]
        ];
    }

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
    /// 
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public EntryResult Move(CommandParserResultMove move)
    {
        Piece? space = Board[move.Start.Rank][move.Start.File];

        if (space == null)
        {
            return new EntryResultError("Invalid Space.");
        }

        switch (space.CanMove(move.End, Board))
        {
            case CanMoveResultValid:
                Board[move.Start.Rank][move.Start.File] = null;
                Board[move.End.Rank][move.End.File] = space.Move(move.End);
                return new EntryResultValid();

            case CanMoveResultEnPassant enPassant:
                Board[move.Start.Rank][move.Start.File] = null;
                Board[enPassant.Position.Rank][enPassant.Position.File] = null;
                Board[move.End.Rank][move.End.File] = space.Move(move.End);
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
    /// 
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public EntryResult Promote(CommandParserResultPromotion move)
    {
        Piece? space = Board[move.Start.Rank][move.Start.File];

        if (space == null)
        {
            return new EntryResultError("Invalid Space.");
        }

        switch (space.CanMove(move.End, Board))
        {
            case CanMoveResultPromote:
                Console.WriteLine("Promote");
                Board[move.Start.Rank][move.Start.File] = null;
                Board[move.End.Rank][move.End.File] = CreatePieceByChar(move.Promotion, space.Color, move.End.Rank, move.End.File);
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
    /// <param name="game"></param>
    public void Render(Game game)
    {
        bool running = false;
        EntryResult previousEntry = new EntryResultValid();

        while (!running)
        {
            Display.Draw(Board, previousEntry);
            Console.Write("Enter Move or Command: ");
            string? response = Console.ReadLine();

            if (response == null)
            {
                continue;
            }

            CommandParserResult result = CommandParser.Parse(response);

            switch (result)
            {
                case CommandParserResultMove move:
                    previousEntry = Move(move);
                    break;

                case CommandParserResultPromotion move:
                    previousEntry = Promote(move);
                    break;

                case CommandParserResultError error:
                    previousEntry = new EntryResultError(error.Message);
                    break;
            }
        }
    }


}