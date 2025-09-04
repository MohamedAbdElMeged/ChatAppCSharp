using Chat_CSharp.Data;
using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public class MessageService : IMessageService
{
    private readonly AppDbContext _context;
    public MessageService()
    {
        _context = new AppDbContext();
    }
    public Message CreateMessage(string chatId, string content)
    {
        Message message = new Message()
        {
            ChatId = chatId,
            Content = content,
            SenderId = InMemoryData.CurrentUser.Id
        };
        _context.Messages.Add(message);
        _context.SaveChanges();
        return message;
    }

    public List<Message> GetMessagesByChat(string chatId)
    {
        return _context.Messages.Where(m => m.ChatId == chatId).OrderBy(m => m.TimeStamp).ToList();
    }
}