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
            [.. Enumerable.Repeat<Piece?>(null, 8)],
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
        }
    }
}