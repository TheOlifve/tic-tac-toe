namespace Tic_Tac_Toe;

public class Grid
{
    private char[] _grid = {'0', '1', '2',
                            '3', '4', '5',
                            '6', '7', '8'};
    private int[,] _winCombinations =
    {
        { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, {0, 3, 6}, 
        { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 }
    };
    private int _currentPosition = 4;
    
    public bool CheckWin(char sign)
    {
        for (int i = 0; i < 8; ++i)
        {
            if (_grid[_winCombinations[i, 0]] == sign &&
                _grid[_winCombinations[i, 1]] == sign &&
                _grid[_winCombinations[i, 2]] == sign)
                return true;
        }
        return false;
    }
    
    public void PrintCell(int cell)
    {
        if (_currentPosition == cell)
            Console.BackgroundColor = ConsoleColor.DarkGray;
        else
            Console.ResetColor();
        
        switch (_grid[cell])
        {
            case 'X':
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case 'O':
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                break;
        }
        
        Console.Write(_grid[cell]);
        Console.ResetColor();
    }

    public bool ChangeCell(char sign)
    {
        if (_grid[_currentPosition] == 'X' || _grid[_currentPosition] == 'O')
            return false;
        _grid[_currentPosition] = sign;
        return true;
    }

    public bool HandleMove(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case (ConsoleKey.UpArrow):
                _currentPosition = _currentPosition < 3 ? _currentPosition + 6 : _currentPosition - 3;
                break;
            case (ConsoleKey.DownArrow):
                _currentPosition = _currentPosition > 5 ? _currentPosition - 6 : _currentPosition + 3;
                break;
            case (ConsoleKey.RightArrow):
                _currentPosition = (_currentPosition + 1) % 3 == 0  ? _currentPosition - 2 : _currentPosition + 1;
                break;
            case (ConsoleKey.LeftArrow):
                _currentPosition = (_currentPosition % 3 == 0 ? _currentPosition + 2 : _currentPosition - 1);
                break;
        }
        return true;
    }

    public void ClearGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            _grid[i] = (char)(i + '0');
        }
    }
}