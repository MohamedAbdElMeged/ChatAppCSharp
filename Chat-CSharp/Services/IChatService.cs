using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public interface IChatService
{
    public string CreateChat(User secondParty);
    public Chat GetChat(String chatId);

    public Chat GetChat(User firstParty, User secondParty);
    public bool IsChatExists(User firstParty, User secondParty);
}