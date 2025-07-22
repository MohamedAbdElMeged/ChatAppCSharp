using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public interface IMessageService
{
    public Message CreateMessage(String chatId, String content);
    public List<Message> GetMessagesByChat(string chatId);
}