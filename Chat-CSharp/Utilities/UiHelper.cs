using Chat_CSharp.Application;

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
    
    
    public void HandleOptions(Menu menu)
    {
        var options = menu.MenuItems.Select(m => m.Name).ToList();
        var optionsName = menu.Name;
        ShowOptions(options,optionsName);
        var key = Console.ReadKey(intercept: true).Key;
        var keys = menu.MenuItems.Select(m => m.Key).ToList();
        if (keys.Exists(k=> k == key))
        {

            menu.MenuItems.First(m => m.Key == key).Action();
        }
        else
        {
            PrintErrorMessage("Invalid Input");
        }
    }
}

