using Art_Gallery.Models;
using Art_Gallery.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Art_Gallery.Controllers;

[ApiController]
[Route("api/artstyle")]
public class ArtStyleController : ControllerBase
{

    private readonly IArtStylesEF _artstyleRepo;

    public ArtStyleController(IArtStylesEF ArtStyleRepo)
    {
        _artstyleRepo = ArtStyleRepo;
    }

    [Authorize(Policy = "UserOnly")]
    [HttpGet]
    public IEnumerable<ArtStyle> GetAllArt() => _artstyleRepo.GetArtStyles();


    [Authorize(Policy = "UserOnly")]
    [HttpGet("{id}", Name = "GetArtStyle")]
    public IActionResult GetArtStyleById(int id)
    {

        var search = _artstyleRepo.GetArtStyles().Exists(e => e.StyleId == id);

        if (search)
        {
            var result = _artstyleRepo.GetArtStyles().First(x => x.StyleId.Equals(id));
            return Ok(result);
        }


        return NotFound("No such record found!");
    }


    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public IActionResult AddArtStyle(ArtStyle newArtStyle)
    {
        var search = _artstyleRepo.GetArtStyles().Exists(e => e.Name == newArtStyle.Name);

        if (search)
        {
            return Conflict("ArtStyle Already exists!");
        }

        _artstyleRepo.AddArtStyle(newArtStyle);

        return CreatedAtRoute("GetArtStyle", new {id = newArtStyle.StyleId}, newArtStyle);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public IActionResult UpdateArtStyle(int id, ArtStyle updatedArtStyle)
    {
        var search = _artstyleRepo.GetArtStyles().Exists(e => e.StyleId == id);



        if (search)
        {
            var result = _artstyleRepo.GetArtStyles().First(x => x.StyleId.Equals(id));

            _artstyleRepo.UpdateArtStyle(id, updatedArtStyle);

            return NoContent();
        }




        return BadRequest("No such record found!");
    }




    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult DeleteArtStyle(int id)
    {
        var search = _artstyleRepo.GetArtStyles().Exists(e => e.StyleId == id);

        if (search)
        {

            _artstyleRepo.DeleteArtStyle(id);

            return Ok();

        }

        return NotFound("No such record found!");

    }





}