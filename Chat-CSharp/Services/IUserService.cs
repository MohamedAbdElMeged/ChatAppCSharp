using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public interface IUserService
{
    public bool IsUserExists(String email);
    public User GetUser(String email);
    public void Register(String email, String password);
    public void AddUserInData(User user);
    public void Login(String email, String password);
    public bool IsEmailValid(string email);

}