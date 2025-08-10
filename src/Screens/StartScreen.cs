class StartScreen : IScreen
{
	public void Render(Game game)
	{
		Console.Clear();
		Display.VerticalPadding(3);
		Display.Title();
		Display.VerticalPadding(3);
		Console.Write("Press any key to continue...");
		Console.ReadKey();
		game.Screen = new GameScreen();
	}
}
