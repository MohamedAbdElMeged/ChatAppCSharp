using Chat_CSharp.Data;
using Chat_CSharp.Models;
using Chat_CSharp.Services;
using Chat_CSharp.Utilities;

namespace Chat_CSharp.Application;

public class ChatApp
{
    private UiHelper _uiHelper = new UiHelper();
    private IUserService _userService = new UserService();
    private IChatService _chatService = new ChatService();
    private IMessageService _messageService = new MessageService();
    public void Start()
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

        var running = true;
        while (running)
        {
            if (InMemoryData.CurrentUser != null)
            {
                Console.WriteLine($"Welcome {InMemoryData.CurrentUser.Email}");
                if (InMemoryData.CurrentUser.Chats().Count == 0)
                {
                    Console.WriteLine("No chats yet.");
                    List<string> options = new List<string>() { "Create New Chat", "Q to Quit", "L to Logout" };
                    
                    Dictionary<ConsoleKey, Action> actions = new Dictionary<ConsoleKey, Action>()
                    {
                        { ConsoleKey.D1, CreateNewChatUi },
                        { ConsoleKey.L, LogoutUi },
                        {
                            ConsoleKey.Q, () =>
                            {
                                Console.WriteLine("Good Bye!");
                                running = false;
                                Environment.Exit(0);
                            }

                        }
                    };
                   
                    _uiHelper.HandleOptions(actions,options,"Logged In");
                }
                else
                {
                    ListAvailableChats();
                    List<string> options = new List<string>() { "Create New Chat","Open Existing Chat", "Q to Quit","L to Logout" };
                    
                    Dictionary<ConsoleKey, Action> actions = new Dictionary<ConsoleKey, Action>()
                    {
                        { ConsoleKey.D1, CreateNewChatUi },
                        { ConsoleKey.D2, OpenExistingChatUi },
                        { ConsoleKey.L, LogoutUi },

                        {
                            ConsoleKey.Q, () =>
                            {
                                Console.WriteLine("Good Bye!");
                                running = false;
                                Environment.Exit(0);
                            }

                        }
                    };
                   
                    _uiHelper.HandleOptions(actions,options,"Logged In");
                }
              
                
                
                
                break;
            }
            else
            {
                List<string> options = new List<string>() { "Register", "Login", "Q to Quit" };
                _uiHelper.ShowOptions(options, "Main Menu Options");
                Dictionary<ConsoleKey, Action> actions = new Dictionary<ConsoleKey, Action>()
                {
                    { ConsoleKey.D1, RegisterUi },
                    { ConsoleKey.D2, LoginUi },
                    {
                        ConsoleKey.Q, () =>
                        {
                            Console.WriteLine("Good Bye!");
                            running = false;
                            Environment.Exit(0);
                        }

                    }
                };
                var key = Console.ReadKey(intercept: true).Key;
                if (actions.ContainsKey(key))
                {

                    actions[key]();
                }
            }
        }

    }

    private void LogoutUi()
    {
        InMemoryData.CurrentUser = null;
        Start();
    }

    private void OpenExistingChatUi()
    {
        Console.WriteLine("Please Enter you chat email buddy");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput) ||
            (!String.IsNullOrEmpty(emailInput) && !_userService.IsUserExists(emailInput)))
        {
            _uiHelper.PrintErrorMessage("you should add valid email");
            Start();
        }
        else
        {
            var userChatParty = _userService.GetUser(emailInput);
            var chat = _chatService.GetChat(InMemoryData.CurrentUser, userChatParty);
            HandleChatUi(chat.Id);
        }
    }

    private void CreateNewChatUi()
    {
        Console.WriteLine("Please enter email of your chat buddy");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput) ||
            (!String.IsNullOrEmpty(emailInput) && !_userService.IsUserExists(emailInput)))
        {
            _uiHelper.PrintErrorMessage("you should add valid email");
            Start();
        }
        else
        {
            var userChatParty = _userService.GetUser(emailInput);
            var chatId= _chatService.CreateChat(userChatParty);
            HandleChatUi(chatId);
        }
    }

    private void HandleChatUi(String chatId)
    {
        var chat = _chatService.GetChat(chatId);
        Console.ForegroundColor = ConsoleColor.Cyan;
        ListOldMessagesForChat(chatId);
        Console.WriteLine($"Chat Between {chat.FirstParty.Email} and {chat.SecondParty.Email} \n");
        Console.ResetColor();
        Console.WriteLine("Start sending Messages..");
        var messageContent = Console.ReadLine();
        _messageService.CreateMessage(chat.Id, messageContent);
        Start();
    }
    private void RegisterUi()
    {
        Console.WriteLine("please enter your email");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput))
        {
         _uiHelper.PrintErrorMessage("you should add valid email");
         Start();
        }
        else
        {
            Console.WriteLine("please enter your password");
            var passwordInput = Console.ReadLine();
            if (String.IsNullOrEmpty(passwordInput))
            {
                _uiHelper.PrintErrorMessage("you should add valid password");
                Start();
            }
            else
            {
                try
                {
                    _userService.Register(emailInput, passwordInput);
                }
                catch(Exception ex)
                {
                    _uiHelper.PrintErrorMessage(ex.Message);
                }
                
                
            }
        }
        
        
    }

    public void ListOldMessagesForChat(string chatId)
    {
        var chat = _chatService.GetChat(chatId);
        var messages = _messageService.GetMessagesByChat(chatId);
        // current user cyan, second party magenta
        foreach (var message in messages)
        {
            if (message.Sender == InMemoryData.CurrentUser)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"{message.Content} , {message.TimeStamp.ToShortDateString()}, {message.TimeStamp.ToShortTimeString()}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"{message.Content} , {message.TimeStamp.ToShortDateString()}, {message.TimeStamp.ToShortTimeString()}");
                Console.ResetColor();
            }
        }
    }
    public void LoginUi()
    {
        Console.WriteLine("please enter your email");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput))
        {
            _uiHelper.PrintErrorMessage("you should add valid email");
            Start();
        }
        else
        {
            Console.WriteLine("please enter your password");
            var passwordInput = Console.ReadLine();
            if (String.IsNullOrEmpty(passwordInput))
            {
                _uiHelper.PrintErrorMessage("you should add valid password");
                Start();
            }
            else
            {
                _userService.Login(emailInput,passwordInput);
                
            }
        }
    }

    public void ListAvailableChats()
    {
        Console.WriteLine("Your Available Chats");
        var chats = InMemoryData.CurrentUser.Chats();
        foreach (var chat in chats)
        {
            User chatParty = null;
            var messages = _messageService.GetMessagesByChat(chat.Id);
            var lastMessage = messages.LastOrDefault();
            if (chat.FirstParty == InMemoryData.CurrentUser)
            {
                chatParty = chat.SecondParty;
            }
            else
            {
                chatParty = chat.FirstParty;
            }
            Console.WriteLine($"Email: {chatParty.Email}, Last Message: {lastMessage.Content} , At {lastMessage.TimeStamp.ToShortDateString()}, {lastMessage.TimeStamp.ToShortTimeString()} ");
        }
    }
 }