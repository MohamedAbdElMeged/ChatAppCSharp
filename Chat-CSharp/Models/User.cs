using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chat_CSharp.Data;

namespace Chat_CSharp.Models;

public class User
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public ICollection<Chat> Chats { get; set; } = new List<Chat>();

    
    // public List<Chat> Chats()
    // {
    //     return InMemoryData.Chats.Where(c => c.FirstParty == InMemoryData.CurrentUser || c.SecondParty == InMemoryData.CurrentUser).ToList();
    //
    // }
    public User(String email, String password)
    {
        Email = email;
        Password = password;
    }
    
    
}