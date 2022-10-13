using System;
using System.Collections.Generic;

namespace Art_Gallery.Models
{
    public class ArtStyle
    {
        public int StyleId { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
