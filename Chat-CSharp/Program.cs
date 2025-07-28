using Chat_CSharp.Application;
using Chat_CSharp.Data;
using Chat_CSharp.Models;
using Chat_CSharp.Services;

namespace Chat_CSharp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome To Chat App");
        ChatApp chatApp = new ChatApp();
        ApplicationDataSeeder.SeedMenuAndMenuActions();
        if (!chatApp.DataExists())
        {
            ApplicationDataSeeder.SeedUserAndChats();
        }

        try
        {
            bool running = true;
            while (running)
            {
                running = chatApp.Start();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            
        }

        
    }
}