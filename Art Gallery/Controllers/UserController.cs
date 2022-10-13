using Art_Gallery.Models;
using Art_Gallery.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Art_Gallery.Controllers;


    [ApiController]
    
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        
        private readonly IUserDataAccess _userRepo;
    
        public UserController(IUserDataAccess userRepo)
        {
            _userRepo = userRepo;
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IEnumerable<User> GetUsers() => _userRepo.GetUsers();    
    
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUserById(int id)
        {
           
           var search = _userRepo.GetUsers().Exists(e => e.Id == id);
    
            if (search)
            {
                var result = _userRepo.GetUsers().First(x => x.Id.Equals(id));
                return Ok(result);
            }
            
            
            return NotFound("No such record found!");
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public IActionResult AddUser(User newUser)
        {
            var search = _userRepo.GetUsers().Exists(e => e.LastName == newUser.LastName);
    
            if (search)
            {
                return Conflict("User Already exists!");
            }
    
            _userRepo.AddUser(newUser);
            
            return CreatedAtRoute("GetUser", new {id = newUser.Id}, newUser);
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var search = _userRepo.GetUsers().Exists(e => e.Id == id);
            
            
            
            if (search)
            {
                var result = _userRepo.GetUsers().First(x => x.Id.Equals(id));
                
                _userRepo.UpdateUser(id, updatedUser);
    
                return NoContent();
            }
            
            
            
            
            return BadRequest("No such record found!");
        }
    
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var search = _userRepo.GetUsers().Exists(e => e.Id == id);

            if (search)
            {

                _userRepo.DeleteUser(id);

                return Ok();

            }
        
            return NotFound("No such record found!");
        
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{role}", Name = "GetAdmin")]
        public IEnumerable<User> GetAllAdmin() => _userRepo.GetAllAdmin();
        
        
        
        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{id}")]
        public IActionResult UpdatePassword(int id, LoginModel updatedUser)
        {
            var search = _userRepo.GetUsers().Exists(e => e.Id == id);
            
            
            
            if (search)
            {
                var result = _userRepo.GetUsers().First(x => x.Id.Equals(id));
                
               
                _userRepo.UpdatePassword(id, updatedUser);
    
                return NoContent();
            }
            
            return BadRequest("No such record found!");

        }
    
}