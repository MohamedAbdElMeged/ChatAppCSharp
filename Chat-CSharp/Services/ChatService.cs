using Chat_CSharp.Data;
using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public class ChatService : IChatService
{
    public string CreateChat(User secondParty)
    {
        var firstParty = InMemoryData.CurrentUser;
        if (IsChatExists(firstParty,secondParty))
        {
            return GetChat(firstParty, secondParty).Id;
        }
        var chat = new Chat()
        {
            FirstParty = firstParty,
            SecondParty = secondParty
        };
        InMemoryData.Chats.Add(chat);
        return chat.Id;
    }

    public Chat GetChat(String chatId)
    {
        return InMemoryData.Chats.First(c => c.Id.ToString() == chatId);
    }

    public Chat GetChat(User firstParty,User secondParty)
    {
        if (!IsChatExists(firstParty,secondParty))
        {
            throw new Exception("Chat not found");
        }
        return InMemoryData.Chats.First(c =>  (c.FirstParty == firstParty && c.SecondParty == secondParty) || 
                                              (c.FirstParty == secondParty && c.SecondParty == firstParty));
    }

    public bool IsChatExists(User firstParty, User secondParty)
    {
        return InMemoryData.Chats.Exists(c => (c.FirstParty == firstParty && c.SecondParty == secondParty) || 
                                              (c.FirstParty == secondParty && c.SecondParty == firstParty)    );
    }
}