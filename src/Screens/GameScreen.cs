class GameScreen : IScreen
{
    Piece?[][] Board { get; init; }

    public GameScreen()
    {
        this.Board = [
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(new Pawn(true), 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)],
            [.. Enumerable.Repeat<Piece?>(new Pawn(false), 8)],
            [.. Enumerable.Repeat<Piece?>(null, 8)]
        ];
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
                    Console.WriteLine($"Start:{move.Start.Rank},{move.Start.File} / End: {move.End.Rank},{move.End.File}");
                    break;

                case ErrorCommandParserResult error:
                    Console.WriteLine(error.Message);
                    break;

                default:
                    break;
            }
        }
    }
}