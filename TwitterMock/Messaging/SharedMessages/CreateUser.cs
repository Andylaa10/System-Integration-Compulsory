namespace Messaging.SharedMessages;

public class CreateUser
{
    public string Message { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    
    public CreateUser(string message, string username, string email, string password, string firstName, string lastName,
        string role = "User")
    {
        Message = message;
        Username = username;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }
}