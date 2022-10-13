


using Art_Gallery.Models;
using Art_Gallery.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Art_Gallery.Controllers;


[ApiController]
[Route("api/artists")]
public class ArtistController : ControllerBase
{
    
    private readonly IArtistsEF _artistsRepo;

    public ArtistController(IArtistsEF artistsRepo)
    {
        _artistsRepo = artistsRepo;
    }
    
    [Authorize(Policy = "UserOnly")]
    [HttpGet]
    public IEnumerable<Artist> GetAllArtists() => _artistsRepo.GetArtists();    

    [Authorize(Policy = "UserOnly")]
    [HttpGet("{id}", Name = "GetArtist")]
    public IActionResult GetArtistById(int id)
    {
       
       var search = _artistsRepo.GetArtists().Exists(e => e.ArtistId == id);

        if (search)
        {
            var result = _artistsRepo.GetArtists().First(x => x.ArtistId.Equals(id));
            return Ok(result);
        }
        
        
        return NotFound("No such record found!");
    }
    
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public IActionResult AddArtist(Artist newArtist)
    {
        var search = _artistsRepo.GetArtists().Exists(e => e.LastName == newArtist.LastName);

        if (search)
        {
            return Conflict("Artist Already exists!");
        }

        _artistsRepo.AddArtist(newArtist);
        
        return CreatedAtRoute("GetArtist", new {id = newArtist.ArtistId}, newArtist);
    }
    
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public IActionResult UpdateArtist(int id, Artist updatedArtist)
    {
        var search = _artistsRepo.GetArtists().Exists(e => e.ArtistId == id);
        
        
        
        if (search)
        {
            var result = _artistsRepo.GetArtists().First(x => x.ArtistId.Equals(id));
            
            _artistsRepo.UpdateArtist(id, updatedArtist);

            return NoContent();
        }
        
        
        
        
        return BadRequest("No such record found!");
    }


   
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult DeleteArtist(int id)
    {
        var search = _artistsRepo.GetArtists().Exists(e => e.ArtistId == id);

        if (search)
        {

            _artistsRepo.DeleteArtist(id);

            return Ok();

        }
        
        return NotFound("No such record found!");
        
    }
    
    
    
    
    
}