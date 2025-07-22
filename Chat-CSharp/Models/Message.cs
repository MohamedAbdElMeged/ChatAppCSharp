namespace Chat_CSharp.Models;

public class Message
{
    public String ChatId { get; set; }
    public DateTime TimeStamp { get; }
    public String Content { get; set; }
    public User Sender { get; set; }

    public String SenderName => Sender.Email;

    public Message()
    {
        TimeStamp = DateTime.Now;
    }
}