namespace Art_Gallery.Models;

public class LoginModel
{

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public LoginModel(string email, string password)
    {
        Email = email;
        Password = password;
    }

}