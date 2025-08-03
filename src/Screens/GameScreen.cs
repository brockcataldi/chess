class GameScreen : IScreen
{
    Piece?[][] Board { get; init; }

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

    public EntryResult Move(CommandParserResultMove move)
    {
        Piece? space = this.Board[move.Start.Rank][move.Start.File];

        if (space == null)
        {
            return new EntryResultError("Invalid Space");
        }

        CanMoveResult canMove = space.CanMove(move.End, this.Board);

        switch (canMove) {
            case CanMoveResultValid:
                Board[move.Start.Rank][move.Start.File] = null;
                Board[move.End.Rank][move.End.File] = space.Move(move.End);
                return new EntryResultValid();
            case CanMoveResultEnPassant enPassant:
                Board[move.Start.Rank][move.Start.File] = null;
                Board[enPassant.Position.Rank][enPassant.Position.File] = null;
                Board[move.End.Rank][move.End.File] = space.Move(move.End);
                return new EntryResultValid();
            case CanMoveResultError error:
                return new EntryResultError(error.Message);
        }

        return new EntryResultError("Invalid Move");
    }

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

                case CommandParserResultError error:
                    previousEntry = new EntryResultError(error.Message);
                    break;

                default:
                    break;
            }
        }
    }
}