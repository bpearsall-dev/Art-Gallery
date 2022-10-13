
using Art_Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.Persistence;

public interface IArtStylesEF
{
    List<ArtStyle> GetArtStyles();
    ArtStyle AddArtStyle(ArtStyle newArtStyle);
    ArtStyle UpdateArtStyle(int id, ArtStyle updatedArtStyle);
    void DeleteArtStyle(int id);
}

public class ArtStylesEF : IArtStylesEF
{
    private GalleryContext _context;
    
    public ArtStylesEF()
    {
        _context = new GalleryContext(new DbContextOptions<GalleryContext>());
    }
    
    
    public List<ArtStyle> GetArtStyles()
    {
        return _context.ArtStyles.ToList();
    } 
    
    
    public ArtStyle AddArtStyle(ArtStyle newArtStyle)
    {
        newArtStyle.CreatedDate = DateTime.Now;
        newArtStyle.ModifiedDate = DateTime.Now;
        _context.ArtStyles.Add(newArtStyle);
        _context.SaveChanges();
       
        return newArtStyle;


    }
    
    
    public ArtStyle UpdateArtStyle(int id, ArtStyle updatedArtStyle)
    {
       
        
        var current = _context.ArtStyles.First(e => e.StyleId == id);
        _context.ArtStyles.Update(current);
        current.Name = updatedArtStyle.Name;
        current.Url = updatedArtStyle.Url;
        current.ModifiedDate = DateTime.Now;
        _context.SaveChanges();


        return updatedArtStyle;
    }



    public void DeleteArtStyle(int id)
    {



        using (_context = new GalleryContext(new DbContextOptions<GalleryContext>()))
        {

            var dlt = _context.ArtStyles.First(e => e.StyleId == id);
            _context.ArtStyles.Remove(dlt);
            _context.SaveChanges();
            


        }
    }
}