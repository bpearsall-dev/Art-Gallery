
using Art_Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.Persistence;

public interface IArtworkEF
{
    List<Artwork> GetArtworks();
    Artwork AddArtwork(Artwork newArtwork);
    Artwork UpdateArtwork(int id, Artwork updatedArtwork);
    void DeleteArtwork(int id);
}

public class ArtworkEF : IArtworkEF
{
    
    private GalleryContext _context;
    
    public ArtworkEF()
    {
        _context = new GalleryContext(new DbContextOptions<GalleryContext>());
    }
    
    
    public List<Artwork> GetArtworks()
    {
        return _context.Artworks.ToList();
    } 
    
    
    public Artwork AddArtwork(Artwork newArtwork)
    {
        
        newArtwork.CreatedDate = DateTime.Now;
        newArtwork.ModifiedDate = DateTime.Now;
        _context.Artworks.Add(newArtwork);
        _context.SaveChanges();
       
        

        return newArtwork;


    }
    
    
    public Artwork UpdateArtwork(int id, Artwork updatedArtwork)
    {
        var current = _context.Artworks.First(e => e.ArtId == id);
        _context.Artworks.Update(current);
        current.ArtistId = updatedArtwork.ArtistId;
        current.Title = updatedArtwork.Title;
        current.Url = updatedArtwork.Url;
        current.ModifiedDate = DateTime.Now;
        
        
        _context.SaveChanges();

        return updatedArtwork;
    }



    public void DeleteArtwork(int id)
    {



        using (_context = new GalleryContext(new DbContextOptions<GalleryContext>()))
        {

            var dlt = _context.Artworks.First(e => e.ArtId == id);
            _context.Artworks.Remove(dlt);
            _context.SaveChanges();
            



        }

    }


    
    
    
    
    
    
}