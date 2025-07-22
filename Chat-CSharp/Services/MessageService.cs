using Chat_CSharp.Data;
using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public class MessageService : IMessageService
{

    private ChatService _chatService;
    public Message CreateMessage(string chatId, string content)
    {
        Message message = new Message()
        {
            ChatId = chatId,
            Content = content,
            Sender = InMemoryData.CurrentUser
        };
        InMemoryData.Messages.Add(message);
        return message;
    }

    public List<Message> GetMessagesByChat(string chatId)
    {
        return InMemoryData.Messages.Where(m => m.ChatId == chatId).OrderBy(m => m.TimeStamp).ToList();
    }
}