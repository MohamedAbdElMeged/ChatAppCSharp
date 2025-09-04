using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chat_CSharp.Data;

namespace Chat_CSharp.Models;

public class Chat
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public User FirstParty { get; set; }
    public string FirstPartyId { get; set; }
    public string SecondPartyId { get; set; }
    public Chat()
    {
        Id = Guid.NewGuid().ToString();
    }
    
}