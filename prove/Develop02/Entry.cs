public class Entry
{
    public string _date;
    public string _prompt;
    public string _userText;

    public Entry()
    {
        PromptGenerator generator = new PromptGenerator();
        _prompt = generator.Prompt();
        DateTime currentDateTime = DateTime.Now;
        _date =  currentDateTime.ToShortDateString();
    }

    public Entry(string date, string prompt , string userText)
    {
        _date = date;
        _prompt = prompt;
        _userText = userText;
    }

    public void Edit()
    {
        Console.WriteLine($"{_prompt}");
        Console.Write("=> ");
        _userText = Console.ReadLine();
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_prompt}");
        Console.WriteLine($"{_userText}");
    }

}