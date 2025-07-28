using System.Text.RegularExpressions;
using Chat_CSharp.Data;
using Chat_CSharp.Models;

namespace Chat_CSharp.Services;

public class UserService : IUserService
{
    public bool IsUserExists(string email)
    {
        if (!IsEmailValid(email))
        {
            return false;
        }
        return InMemoryData.Users.Exists(u => u.Email.ToLower() == email.ToLower());
    }

    public User GetUser(string email)
    {
        return InMemoryData.Users.First(u => u.Email.ToLower() == email.ToLower());
    }

    public void Register(String email, string password)
    {
        // Check if user exists already
        if (IsUserExists(email))
        {
            throw new Exception("User already Exists!");
        }

        if (!IsEmailValid(email))
        {
            throw new Exception("Email is not valid");
        }
        var user = new User(email, password);
        AddUserInData(user);
        InMemoryData.CurrentUser = user;
    }

    public void AddUserInData(User user)
    {
        InMemoryData.Users.Add(user);
    }

    public void Login(string email, string password)
    {
        // reduce if nesting 
        if (!IsUserExists(email))
        {
            throw new Exception("Invalid Email or Password ");
        }
        
        var user = GetUser(email);
        if (user.Password != password)
        {
            throw new Exception("Invalid Email or Password ");
        }
        InMemoryData.CurrentUser = user;

    }

    public bool IsEmailValid(string email)
    {
        if (String.IsNullOrEmpty(email) )
        {
            return false;
        }
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}