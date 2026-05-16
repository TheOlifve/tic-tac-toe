namespace Tic_Tac_Toe;

using aca_propsss.MenuLib;
public class Settings : Menu
{
    private readonly Player[] _players;
    public Settings(Player[] players) : base("Main Menu")
    {
        _players = players;
        ConfigureOptionSize(2);
        AddOption("1", "Change First Player's Username");
        AddOption("2", "Change Second Player's Username");
    }
    protected override NavigationResult HandleOption(string option)
    {
        switch (option)
        {
            case "1":
                _players[0].ChangeUsername();
                return NavigationResult.Wait();
            case "2":
                _players[1].ChangeUsername();
                return NavigationResult.Wait();
            default:
                return NavigationResult.None();
        }
    }
}
public class GameplaySignSelection : Menu
{
    private readonly GameMode _gameMode;
    private readonly Menu _mainMenu;
    private readonly Game _game;
        
    public GameplaySignSelection(Game game, GameMode gameMode, Menu menu) : base("Sign Selection")
    {
        _game = game;
        _gameMode = gameMode;
        _mainMenu = menu;
        ConfigureOptionSize(2);
        AddOption("1", "X");
        AddOption("2", "O");   
    }
        
    protected override NavigationResult HandleOption(string option)
    {
        switch (option)
        {
            case "1":
                _gameMode.Sign = 'X';
                break;
            case "2":
                _gameMode.Sign = 'O';
                break;
            default:
                break;
        }
        _game.EnterTheGameMode();
        return NavigationResult.GoTo(_mainMenu);
    }
}

public class GameplayModeSelection : Menu
{
    private readonly GameMode _gameMode;
    private readonly Menu _mainMenu;
    private readonly Game _game;
    public GameplayModeSelection(Game game, GameMode gameMode, Menu mainMenu) : base("Gameplay Mode Selection")
    {
        _gameMode = gameMode;
        _mainMenu = mainMenu;
        _game = game;
        ConfigureOptionSize(2);
        AddOption("1", "Player vs Player");
        AddOption("2", "Player vs Computer");
    }
    protected override NavigationResult HandleOption(string option)
    {
        switch (option)
        {
            case "1":
                _gameMode.Pvp = true;
                MenuRunner.Run(new GameplaySignSelection(_game, _gameMode, _mainMenu));
                return NavigationResult.None();
            case "2":
                _gameMode.Pvp = false;
                MenuRunner.Run(new GameplaySignSelection(_game, _gameMode, _mainMenu));
                return NavigationResult.None();
            default:
                return NavigationResult.None();
        }
    }
}

public class MainMenu : Menu
{
    private readonly Player[] _players;
    private readonly GameMode _gameMode;
    private readonly Game     _game;
    public MainMenu(Game game, Player[] players, GameMode gameMode) : base("Main Menu")
    {
        _players = players;
        _gameMode = gameMode;
        _game = game;
        ConfigureOptionSize(4);
        AddOption("1", "Play");
        AddOption("2", "Settings");
        AddOption("3", "About");
        AddOption("4", "Quit");
    }
    
    protected override NavigationResult HandleOption(string option)
    {
        switch (option)
        {
            case "1":
                MenuRunner.Run(new GameplayModeSelection(_game ,_gameMode, this));
                return NavigationResult.None();
            case "2":
                MenuRunner.Run(new Settings(_players));
                return NavigationResult.None();
            case "3":
                About();
                return NavigationResult.Wait();
            case "4":
                return NavigationResult.Exit();
            default:
                return NavigationResult.None();
        }
    }

    private void About()
    {
        Console.Clear();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("║                ABOUT THE GAME              ║");
        Console.WriteLine("╚════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine("       Developed by:  Hrant");
        Console.WriteLine("       Organization:  Tech-Gen");
        Console.WriteLine("       Year:          2026");
        Console.WriteLine();
    }
}