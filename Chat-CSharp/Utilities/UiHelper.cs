namespace Chat_CSharp.Utilities;

public class UiHelper
{
    public void ShowOptions(List<string> options, string optionsName)
    {
        Console.WriteLine($"{optionsName} \n");
        for(int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {options[i]}");
        }
    }

    public void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void HandleOptions(Dictionary<ConsoleKey, Action> actions, List<string> options, string optionsName)
    {
        ShowOptions(options, optionsName);
        var key = Console.ReadKey(intercept: true).Key;
        if (actions.ContainsKey(key))
        {

            actions[key]();
        }
    }
}