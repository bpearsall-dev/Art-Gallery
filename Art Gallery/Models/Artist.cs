using System;
using System.Collections.Generic;

namespace Art_Gallery.Models
{
    public class Artist
    {
        public Artist()
        {
            Artworks = new HashSet<Artwork>();
        }

        public int ArtistId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int YearBorn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
    }
}
