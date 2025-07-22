using Chat_CSharp.Data;

namespace Chat_CSharp.Models;

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }
    

    
    public List<Chat> Chats()
    {
        return InMemoryData.Chats.Where(c => c.FirstParty == InMemoryData.CurrentUser || c.SecondParty == InMemoryData.CurrentUser).ToList();

    }
    public User(String email, String password)
    {
        Email = email;
        Password = password;
    }
    
    
}