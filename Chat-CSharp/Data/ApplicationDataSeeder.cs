using Chat_CSharp.Application;
using Chat_CSharp.Models;

namespace Chat_CSharp.Data;

public class ApplicationDataSeeder
{


    public static void SeedUserAndChats()
    {
        User user1Test = new User("test@mo.com", password:"123456");
        InMemoryData.Users.Add(user1Test);
        User user2Test = new User("mo@mo.com", password:"123456");
        InMemoryData.Users.Add(user2Test);
        Chat chat = new Chat()
        {
            FirstParty = user1Test,
            SecondParty = user2Test
        };
        InMemoryData.Chats.Add(chat);
        Message message1 = new Message()
        {
            Content = "Hello! ",
            Sender = user1Test,
            ChatId = chat.Id
        };
        Message message2 = new Message()
        {
            Content = "Hello Too! ",
            Sender = user2Test,
            ChatId = chat.Id
        };
        InMemoryData.Messages.Add(message1);
        InMemoryData.Messages.Add(message2);
    }

    public static void SeedMenuAndMenuActions()
    {
        var menuActions = new MenuActions();
        var anonymousMenu = new Menu()
        {
            Name = "Anonymous",
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Action = menuActions.RegisterUi,
                    Key = ConsoleKey.D1,
                    Name = "Register"
                },
                new MenuItem()
                {
                    Action = menuActions.LoginUi,
                    Key = ConsoleKey.D2,
                    Name = "Login"
                }
            }
        };

        var loggedInWithOutChatsMenu = new Menu()
        {
            Name = "Logged In Without Chats",
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Action = menuActions.CreateNewChatUi,
                    Key = ConsoleKey.D1,
                    Name = "Create New Chat"
                },
                new MenuItem()
                {
                    Action = menuActions.LogoutUi,
                    Key = ConsoleKey.L,
                    Name = "Logout"
                }
            }
        };

        
        var loggedInWithChatsMenu = new Menu()
        {
            Name = "Logged In With Chats",
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Action = menuActions.CreateNewChatUi,
                    Key = ConsoleKey.D1,
                    Name = "Create New Chat"
                },
                new MenuItem()
                {
                    Action = menuActions.OpenExistingChatUi,
                    Key = ConsoleKey.D2,
                    Name = "Open Existing Chat"
                },
                new MenuItem()
                {
                    Action = menuActions.LogoutUi,
                    Key = ConsoleKey.L,
                    Name = "Logout"
                }
            }
        };
        
        InMemoryData.Menus.Add(anonymousMenu);
        InMemoryData.Menus.Add(loggedInWithOutChatsMenu);
        InMemoryData.Menus.Add(loggedInWithChatsMenu);

        // menu.Actions = new Dictionary<ConsoleKey, Action>()
        // {
        //     { ConsoleKey.D1,  menuActions.RegisterUi },
        //     { ConsoleKey.D2,  menuActions.LoginUi },
        //     { ConsoleKey.L,  menuActions.LogoutUi }
        // };
        // InMemoryData.Menus.Add(menu);
        // menu = new Menu();
        // menu.Name = "Logged In without Chats";
        // menu.Actions = new Dictionary<ConsoleKey, Action>()
        // {
        //     { ConsoleKey.D1,  menuActions.CreateNewChatUi },
        //     { ConsoleKey.L,  menuActions.LogoutUi }
        // };
        // InMemoryData.Menus.Add(menu);
        // menu = new Menu();
        // menu.Name = "Logged In with Chats";
        // menu.Actions = new Dictionary<ConsoleKey, Action>()
        // {
        //     { ConsoleKey.D1,  menuActions.CreateNewChatUi },
        //     { ConsoleKey.D2,  menuActions.OpenExistingChatUi },
        //     { ConsoleKey.L,  menuActions.LogoutUi }
        // };
        // InMemoryData.Menus.Add(menu);

    }
}