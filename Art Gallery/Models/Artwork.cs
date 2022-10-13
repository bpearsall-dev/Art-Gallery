using System;
using System.Collections.Generic;

namespace Art_Gallery.Models
{
    public class Artwork
    {
        public int ArtId { get; set; }
        public string Title { get; set; } = null!;
        public int ArtistId { get; set; }
        public string Url { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Artist Artist { get; set; } = null!;
    }
}
