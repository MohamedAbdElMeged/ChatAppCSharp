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

        chatApp.Start();

     // try catch inside each method in chat app

    }
}