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
   
    public bool Start()
    {
        if (InMemoryData.CurrentUser == null)
        {
            var anonymousMenu = InMemoryData.Menus.First(m => m.Name == "Anonymous");
          _uiHelper.HandleOptions(anonymousMenu);
          return true;
        }

        if (HasChats())
        {
            var hasChatsMenu = InMemoryData.Menus.First(m => m.Name == "Logged In With Chats");
            _uiHelper.HandleOptions(hasChatsMenu);
            return true;
        }

        var notHavingChats = InMemoryData.Menus.First(m => m.Name == "Logged In Without Chats");
        _uiHelper.HandleOptions(notHavingChats);
        return true;
    }

    private static bool HasChats()
    {
        return InMemoryData.CurrentUser.Chats().Count >= 1;
    }

    public bool DataExists()
    {
        return InMemoryData.Users.Count > 0;
    }
 }
 
 