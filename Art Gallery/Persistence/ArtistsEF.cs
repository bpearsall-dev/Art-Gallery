
using Art_Gallery.Models;
using Microsoft.EntityFrameworkCore;
using Art_Gallery.Controllers;

namespace Art_Gallery.Persistence;

public interface IArtistsEF
{
    List<Artist> GetArtists();
    Artist AddArtist(Artist newArtist);
    Artist UpdateArtist(int id, Artist updatedArtist);
    void DeleteArtist(int id);
}

public class ArtistsEF : IArtistsEF
{
    
    private GalleryContext _context;
    
    public ArtistsEF()
    {
        _context = new GalleryContext(new DbContextOptions<GalleryContext>());
    }
    
    
    public List<Artist> GetArtists()
    {
        return _context.Artists.ToList();
    } 
    
    
    public Artist AddArtist(Artist newArtist)
    {
        newArtist.CreatedDate = DateTime.Now;
        newArtist.ModifiedDate = DateTime.Now;
        _context.Artists.Add(newArtist);
        _context.SaveChanges();
       
        return newArtist;


    }
    
    
    public Artist UpdateArtist(int id, Artist updatedArtist)
    {
        var current = _context.Artists.First(e => e.ArtistId == id);
        _context.Artists.Update(current);
        current.FirstName = updatedArtist.FirstName;
        current.LastName = updatedArtist.LastName;
        if(updatedArtist.YearBorn > 0)
        {
            current.YearBorn = updatedArtist.YearBorn;
        }
        current.ModifiedDate = DateTime.Now;
        current.Artworks = updatedArtist.Artworks;
       
        _context.SaveChanges();
        

        return updatedArtist;
    }



    public void DeleteArtist(int id)
    {



        using (_context = new GalleryContext(new DbContextOptions<GalleryContext>()))
        {

            var dlt = _context.Artists.First(e => e.ArtistId == id);
            _context.Artists.Remove(dlt);
            _context.SaveChanges();
            

        }

    }








}