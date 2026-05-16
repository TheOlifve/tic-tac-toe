namespace Tic_Tac_Toe;

using aca_propsss.MenuLib;

public class Computer
{
    public char Sign {  get; set; }

    public bool MakeMove(Grid grid)
    {
        
        return false;
    }
}

public class GameMode
{
    public bool Pvp { get; set; }
    public char Sign{ get; set; }
}

public class Game
{
    private int      _moves = 0;
    
    private Computer _computer = new Computer();
    
    private Player   _currentPlayer;
    private Player[] _players = new Player[2];
    
    private Grid     _grid = new Grid();
    private GameMode _gameMode = new GameMode();
    
    
    


    public Game()
    {
        _players[0] = new Player();
        _players[1] = new Player();
        _currentPlayer = _players[0];
    }
    
    public void Start()
    {
        Console.WriteLine("Welcome to Tic Tac Toe!");
        _players[0].ChangeUsername();

        MenuRunner.Run(new MainMenu(this, _players, _gameMode));
    }
    
    public void EnterTheGameMode()
    {
        _players[0].Sign = _gameMode.Sign;
        if (_gameMode.Pvp)
        {
            _players[1].SetUsername();
            _players[1].Sign = _players[0].Sign == 'X' ? 'O' : 'X';
            _currentPlayer = _players[0].Sign == 'X' ? _players[0] : _players[1];
        }
        else
            _computer.Sign = _players[0].Sign == 'X' ? 'O' : 'X';

        Display();
        while (OnPress(Console.ReadKey()))
        {
            Display();
        }
    }

    private void Display()
    {
        Console.Clear();
        //----------------- Print Players Name -----------------
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (_gameMode.Pvp)
            Console.WriteLine($"{_players[0].Username}[{_players[0].Sign}] - " +
                              $"{_players[1].Username}[{_players[1].Sign}]");
        // else
            // Console.WriteLine($"{_players[0].Username}[{_players[0].Sign}] -" +
            //                   $"Computer[{_computer.Sign}]");
        Console.ResetColor();
        Console.WriteLine();
        //------------------------------------------------------

        //----------------- Print grid -----------------
        Console.Write(" ");
        _grid.PrintCell(0);
        Console.Write(" | ");
        _grid.PrintCell(1);
        Console.Write(" | ");
        _grid.PrintCell(2);
        Console.WriteLine();
        Console.WriteLine("----------");
        Console.Write(" ");
        _grid.PrintCell(3);
        Console.Write(" | ");
        _grid.PrintCell(4);
        Console.Write(" | ");
        _grid.PrintCell(5);
        Console.WriteLine();
        Console.WriteLine("----------");
        Console.Write(" ");
        _grid.PrintCell(6);
        Console.Write(" | ");
        _grid.PrintCell(7);
        Console.Write(" | ");
        _grid.PrintCell(8);
        Console.WriteLine();
        //----------------------------------------------

        //----------------- Print Current Turn -----------------
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"Current turn: {_currentPlayer.Username}[{_currentPlayer.Sign}]");
        Console.WriteLine();
        //------------------------------------------------------

        //----------------- Print Instructions -----------------
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Use WASD to move, and Enter to place {_currentPlayer.Sign}, or Q to return to menu.");
        //------------------------------------------------------

        Console.ResetColor();
    }

    private bool checkResults()
    {
        if (_grid.CheckWin(_currentPlayer.Sign) || _moves == 9)
        {
            _grid.ClearGrid();
            Console.Clear();
            Console.ResetColor();

            if (_moves == 9)
                Console.WriteLine($"TIE");
            else
                Console.WriteLine($"WINNER - {_currentPlayer.Username}");

            Console.WriteLine("Waiting..., Press any key to continue...");
            Console.ReadKey();
            _moves = 0;
            return false;
        }
        return true;
    }

    private bool HandleEnter()
    {
        if (_grid.ChangeCell(_currentPlayer.Sign))
        {
            _moves++;

            switch (_gameMode.Pvp)
            {
                case true:
                    if (!checkResults())
                        return false;
                    _currentPlayer = _currentPlayer == _players[0] ? _players[1] : _players[0];
                    break;
                case false:
                    break;
            }
        }
        return true;
    }

    private bool OnPress(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.Enter:
                return HandleEnter();
            case ConsoleKey.Q:
                return false;
            default:
                return _grid.HandleMove(key);
        }
    }
}
