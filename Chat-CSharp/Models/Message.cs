using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chat_CSharp.Models;

public class Message
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public String Id { get; set; }
    public Chat Chat { get; set; }
    public string ChatId { get; set; }
    
    
    public DateTime TimeStamp { get; set; }
    public String Content { get; set; }
    public User Sender { get; set; }
    public string SenderId { get; set; }
    public String SenderName => Sender.Email;

    public Message()
    {
        TimeStamp = DateTime.Now;
    }
}