using Chat_CSharp.Data;
using Chat_CSharp.Models;
using Chat_CSharp.Services;
using Chat_CSharp.Utilities;

namespace Chat_CSharp.Application;

public class MenuActions
{
    private UiHelper _uiHelper = new UiHelper();
    private IUserService _userService = new UserService();
    private IChatService _chatService = new ChatService();
    private IMessageService _messageService = new MessageService();
    
    public void LogoutUi()
    {
        InMemoryData.CurrentUser = null;

    }

    public void OpenExistingChatUi()
    {
        Console.WriteLine("Please Enter you chat email buddy");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput) ||
            (!String.IsNullOrEmpty(emailInput) && !_userService.IsUserExists(emailInput)))
        {
            _uiHelper.PrintErrorMessage("you should add valid email");
        }
        else
        {
            var userChatParty = _userService.GetUser(emailInput);
            var chat = _chatService.GetChat(InMemoryData.CurrentUser, userChatParty);
            HandleChatUi(chat.Id);
        }
    }

    public void CreateNewChatUi()
    {
        Console.WriteLine("Please enter email of your chat buddy");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput) ||
            (!String.IsNullOrEmpty(emailInput) && !_userService.IsUserExists(emailInput)))
        {
            _uiHelper.PrintErrorMessage("you should add valid email");
        }
        else
        {
            var userChatParty = _userService.GetUser(emailInput);
            var chatId= _chatService.CreateChat(userChatParty);
            HandleChatUi(chatId);
        }
    }
    public void HandleChatUi(String chatId)
    {
        var chat = _chatService.GetChat(chatId);
        Console.ForegroundColor = ConsoleColor.Cyan;
        ListOldMessagesForChat(chatId);
        Console.WriteLine($"Chat Between {chat.FirstParty.Email} and {chat.SecondParty.Email} \n");
        Console.ResetColor();
        Console.WriteLine("Start sending Messages..");
        var messageContent = Console.ReadLine();
        _messageService.CreateMessage(chat.Id, messageContent);
    }
    public void RegisterUi()
    {
        Console.WriteLine("please enter your email");
        var emailInput = Console.ReadLine();
        if (String.IsNullOrEmpty(emailInput) || !_userService.IsEmailValid(emailInput))
        {
            _uiHelper.PrintErrorMessage("you should add valid email");

        }
        else
        {
            Console.WriteLine("please enter your password");
            var passwordInput = Console.ReadLine();
            if (String.IsNullOrEmpty(passwordInput))
            {
                _uiHelper.PrintErrorMessage("you should add valid password");
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
    public void ListAvailableChats()
    {
        Console.WriteLine("Your Available Chats");
        var chats = InMemoryData.CurrentUser.Chats();
        foreach (var chat in chats)
        {
            
            var messages = _messageService.GetMessagesByChat(chat.Id);
            var lastMessage = messages.LastOrDefault();
            User chatParty;
            if (chat.FirstParty == InMemoryData.CurrentUser)
            {
                chatParty = chat.SecondParty;
            }
            else
            {
                chatParty = chat.FirstParty;
            }
            
            Console.WriteLine($"Email: {chatParty.Email} , Last Message: {lastMessage.Content} , At {lastMessage.TimeStamp.ToShortDateString()}, {lastMessage.TimeStamp.ToShortTimeString()} ");
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
        }
        else
        {
            Console.WriteLine("please enter your password");
            var passwordInput = Console.ReadLine();
            if (String.IsNullOrEmpty(passwordInput))
            {
                _uiHelper.PrintErrorMessage("you should add valid password");
            }
            else
            {
                _userService.Login(emailInput,passwordInput);
                ListAvailableChats();

            }
        }
    }
}