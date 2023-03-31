public class User
{
  private int _userId;
  private string _username;
  private string _password;

  public User(string username, string password)
  {
    _userId = 0;
    _username = username;
    _password = password;
  }

  public User(int userId, string username, string password)
  {
    _userId = userId;
    _username = username;
    _password = password;
  }

  public int GetId()
  {
    return _userId;
  }

  public string GetUsername()
  {
    return _username;
  }

  public bool CheckPassword(string password)
  {
    return (_password == password);
  }


  public static string HashPassword(string password)
  {
    return password;
  }

}