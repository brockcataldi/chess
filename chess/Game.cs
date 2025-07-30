class Game
{
    private IScreen _screen = new StartScreen();
    public IScreen Screen
    {
        get { return this._screen; }
        set
        {
            this._screen = value;
            this._screen.Render(this);
        }
    }

    public Game()
    {
        
    }

    public void Start()
    {
        this.Screen.Render(this);
    }

}