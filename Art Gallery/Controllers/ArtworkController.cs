using Art_Gallery.Models;
using Art_Gallery.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Art_Gallery.Controllers;

[ApiController]
[Route("api/artwork")]
public class ArtworkController : ControllerBase
{

    private readonly IArtworkEF _artworkRepo;

    public ArtworkController(IArtworkEF artworkRepo)
    {
        _artworkRepo = artworkRepo;
    }

    [Authorize(Policy = "UserOnly")]
    [HttpGet]
    public IEnumerable<Artwork> GetAllArt() => _artworkRepo.GetArtworks();


    [Authorize(Policy = "UserOnly")]
    [HttpGet("{id}", Name = "GetArtwork")]
    public IActionResult GetArtworkById(int id)
    {

        var search = _artworkRepo.GetArtworks().Exists(e => e.ArtId == id);

        if (search)
        {
            var result = _artworkRepo.GetArtworks().First(x => x.ArtId.Equals(id));
            return Ok(result);
        }


        return NotFound("No such record found!");
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public IActionResult AddArtwork(Artwork newArtwork)
    {
        var search = _artworkRepo.GetArtworks().Exists(e => e.Title == newArtwork.Title);

        if (search)
        {
            return Conflict("Artwork Already exists!");
        }

        _artworkRepo.AddArtwork(newArtwork);

        return CreatedAtRoute("GetArtwork", new {id = newArtwork.ArtId}, newArtwork);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public IActionResult UpdateArtist(int id, Artwork updatedArtwork)
    {
        var search = _artworkRepo.GetArtworks().Exists(e => e.ArtId == id);



        if (search)
        {
            var result = _artworkRepo.GetArtworks().First(x => x.ArtId.Equals(id));

            _artworkRepo.UpdateArtwork(id, updatedArtwork);

            return NoContent();
        }


        

        return BadRequest("No such record found!");
    }




    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult DeleteArtwork(int id)
    {
        var search = _artworkRepo.GetArtworks().Exists(e => e.ArtId == id);

        if (search)
        {

            _artworkRepo.DeleteArtwork(id);

            return Ok();

        }

        return NotFound("No such record found!");

    }





}