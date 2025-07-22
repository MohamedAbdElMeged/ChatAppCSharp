using Chat_CSharp.Data;

namespace Chat_CSharp.Models;

public class Chat
{
    public string Id { get; set; }
    public User FirstParty { get; set; }
    public User SecondParty { get; set; }
    
    public Chat()
    {
        Id = Guid.NewGuid().ToString();
    }
    
}