class GameScreen : IScreen
{
    Piece?[][] Board { get; init; }

    public GameScreen()
    {
        // This is hella ugly make this more elegant
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

    public void Move(MoveCommandParserResult move)
    {
        Piece? space = this.Board[move.Start.Rank][move.Start.File];

        if (space == null)
        {
            Console.WriteLine("Invalid Space");
            // Error empty space
            return;
        }

        if (space.CanMove(move.End, this.Board))
        {
            this.Board[move.Start.Rank][move.Start.File] = null;
            space.Move(move.End, this.Board);
            this.Board[move.End.Rank][move.End.File] = space;
            // we'll need to check to see if the king is checked
            return;
        }

        Console.WriteLine("Invalid Move");
        // Error invalid move
        return;
    }

    public void Render(Game game)
    {
        bool running = false;


        while (!running)
        {
            Display.Draw(this.Board);
            Console.Write("Enter Move or Command: ");
            string? response = Console.ReadLine();

            if (response == null)
            {
                continue;
            }

            CommandParserResult result = CommandParser.Parse(response);

            switch (result)
            {
                case MoveCommandParserResult move:
                    Move(move);
                    break;

                case ErrorCommandParserResult error:
                    // Move this to display properly
                    Console.WriteLine(error.Message);
                    break;

                default:
                    break;
            }
        }
    }
}