using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Entities
{
    public class UploadedFile
    {
        public UploadedFile(string filePath, bool isUsed)
        {
            FilePath = filePath;
            IsUsed = isUsed;
        }
        public UploadedFile() { }

        public Guid Id { get; set; }

        public string FilePath { get; set; }

        public bool IsUsed { get; set; }
    }
}
