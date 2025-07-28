using Chat_CSharp.Application;
using Chat_CSharp.Models;

namespace Chat_CSharp.Data;

public static class InMemoryData
{
    public static List<User> Users = new List<User>();
    public static List<Chat> Chats = new List<Chat>();
    public static List<Message> Messages = new List<Message>();
    public static List<Menu> Menus = new List<Menu>();
    public static User CurrentUser { get; set; }
}