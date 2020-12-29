using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Entities
{
    public class Thumbnail
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public UploadedFile File { get; set; }
    }
}
