using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid ThumbnailId { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public Guid FileId { get; set; }
        public UploadedFile File { get; set; }
    }
}
