class StartScreen : IScreen
{
    public void Render(Game game)
    {
        Console.Clear();
        Display.VerticalPadding(3);
        Console.WriteLine("            ('-. .-.   ('-.    .-')     .-')    ");
        Console.WriteLine("           ( OO )  / _(  OO)  ( OO ).  ( OO ).  ");
        Console.WriteLine("   .-----. ,--. ,--.(,------.(_)---\\_)(_)---\\_) ");
        Console.WriteLine("  '  .--./ |  | |  | |  .---'/    _ | /    _ |  ");
        Console.WriteLine("  |  |('-. |   .|  | |  |    \\  :` `. \\  :` `.  ");
        Console.WriteLine(" /_) |OO  )|       |(|  '--.  '..`''.) '..`''.) ");
        Console.WriteLine(" ||  |`-'| |  .-.  | |  .--' .-._)   \\.P._)   \\ ");
        Console.WriteLine("(_'  '--'\\ |  | |  | |  `---.\\       /\\       / ");
        Console.WriteLine("   `-----' `--' `--' `------' `-----'  `-----'  ");
        Display.VerticalPadding(3);
        Console.Write("Press any key to continue...");
        Console.ReadKey();
        game.Screen = new GameScreen();
    }
}