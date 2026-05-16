namespace Tic_Tac_Toe;

public class Player
{
    private bool       _default = true;
    private string     _username;
    private static int _cnt = 0;
    
    public Player ()
    {
        _cnt++;
        _username = "Player" + _cnt.ToString();
    }
    
    public Player (string username)
    {
        _cnt++;
        _default = false;
        _username = username;
    }

    public char Sign {  get; set; }

    public string Username
    {
        get => _username;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) && !_default)
                return;
            if (value.Length < 2 || value.Length > 12 ||  !value.Any(char.IsLetterOrDigit) || value == _username)
                throw new ArgumentException("Username must be between 2 and 12 characters long and can only contain letters and numbers.");
            _username = value;
            _default = false;
        }
    }

    public void SetUsername()
    {
        if (_default == false)
            return;
        ChangeUsername();
    }
    public void ChangeUsername()
    {
        Console.Write($"Please enter username for '{_username}': ");
        try
        {
            Username = Console.ReadLine();
            Console.WriteLine("Username changed to: " + Username);
        }
        catch (ArgumentException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed: {e.Message}");
            Console.ResetColor();
            ChangeUsername();
        }
    }
}