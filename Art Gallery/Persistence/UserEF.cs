using Art_Gallery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.Persistence;

public interface IUserDataAccess
{
    List<User> GetUsers();
    User AddUser(User newUser);
    User UpdateUser(int id, User updatedUser);
    void DeleteUser(int id);
    List<User> GetAllAdmin();
    LoginModel UpdatePassword(int id, LoginModel updatedPassword);
}

public class UserEF : IUserDataAccess
{
    private GalleryContext _context;
    
    public UserEF()
    {
        _context = new GalleryContext(new DbContextOptions<GalleryContext>());
    }
    
    
    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    } 
    
    
    public User AddUser(User newUser)
    {
        
        var password = newUser.PasswordHash;
        var hasher = new PasswordHasher<User>();
        var pwHash = hasher.HashPassword(newUser, password);
        newUser.PasswordHash = pwHash;
        newUser.CreatedDate = DateTime.Now;
        newUser.ModifiedDate = DateTime.Now;
        _context.Users.Add(newUser);
        _context.SaveChanges();
       
        return newUser;


    }
    
    
    public User UpdateUser(int id, User updatedUser)
    {
        var current = _context.Users.First(e => e.Id == id);
        _context.Users.Update(current);
        current.FirstName = updatedUser.FirstName;
        current.LastName = updatedUser.LastName;
        current.Email = updatedUser.Email;
        current.PasswordHash = updatedUser.PasswordHash;
        current.ModifiedDate = DateTime.Now;
       
        _context.SaveChanges();
        

        return updatedUser;
    }



    public void DeleteUser(int id)
    {



        using (_context = new GalleryContext(new DbContextOptions<GalleryContext>()))
        {

            var dlt = _context.Users.First(e => e.Id == id);
            _context.Users.Remove(dlt);
            _context.SaveChanges();
            

        }

    }
    
    public List<User> GetAllAdmin()
    {
        var admin = _context.Users.Where(p => p.Role == "admin");
       
        var result = new List<User>(admin);
        
        
        return result;
    } 
    
    
    public LoginModel UpdatePassword(int id, LoginModel updatedPassword)
    {
        
        var current = _context.Users.First(e => e.Id == id);
        
        var password = updatedPassword.Password;
        var hasher = new PasswordHasher<User>();
        var pwHash = hasher.HashPassword(current, password);
        current.PasswordHash = pwHash;
        current.ModifiedDate = DateTime.Now;
        _context.Users.Update(current);
        _context.SaveChanges();


        return updatedPassword;
    }



}