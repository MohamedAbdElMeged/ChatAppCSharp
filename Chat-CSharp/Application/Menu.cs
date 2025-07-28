namespace Chat_CSharp.Application;

public class Menu
{
    public string Name { get; set; }
    public List<MenuItem> MenuItems = new List<MenuItem>();
}

public class MenuItem
{
    public string Name { get; set; }  
    public ConsoleKey Key { get; set; } 
    public Action Action { get; set; } 
}