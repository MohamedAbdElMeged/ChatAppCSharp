using Chat_CSharp.Data;
using Chat_CSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_CSharp.Services;

public class ChatService : IChatService
{
    private readonly AppDbContext _context;
    public ChatService()
    {
        _context = new AppDbContext();
    }
    public string CreateChat(User secondParty)
    {
       
        var firstParty = InMemoryData.CurrentUser;
        if (IsChatExists(firstParty,secondParty))
        {
            return GetChat(firstParty, secondParty).Id;
        }
        var chat = new Chat()
        {
            FirstPartyId = firstParty.Id,
            SecondPartyId = secondParty.Id
        };
        _context.Chats.Add(chat);
        _context.SaveChanges();
        
        return chat.Id;
    }

    public Chat GetChat(String chatId)
    {
        return _context.Chats.First(c => c.Id == chatId);
    }

    public Chat GetChat(User firstParty,User secondParty)
    {
        if (!IsChatExists(firstParty,secondParty))
        {
            throw new Exception("Chat not found");
        }
        return _context.Chats.First(c =>
            c.FirstPartyId == firstParty.Id && c.SecondPartyId == secondParty.Id ||
            (c.FirstPartyId == secondParty.Id && c.SecondPartyId == firstParty.Id));
    }

    public bool IsChatExists(User firstParty, User secondParty)
    {
        // return InMemoryData.Chats.Exists(c => (c.FirstParty == firstParty && c.SecondParty == secondParty) || 
        //                                       (c.FirstParty == secondParty && c.SecondParty == firstParty)    );
        var chat = _context.Chats.FirstOrDefault(c =>
            c.FirstPartyId == firstParty.Id && c.SecondPartyId == secondParty.Id ||
            (c.FirstPartyId == secondParty.Id && c.SecondPartyId == firstParty.Id));
        return chat != null;
    }

    public List<Chat> GetChatsByUserId(string userId)
    {
        return _context.Chats.Include(u=> u.FirstParty).Where(c=> c.FirstPartyId == userId || c.SecondPartyId == userId).ToList<Chat>();
    }
}