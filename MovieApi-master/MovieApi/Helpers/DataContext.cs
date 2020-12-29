using Microsoft.EntityFrameworkCore;
using MovieApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
    }
}
