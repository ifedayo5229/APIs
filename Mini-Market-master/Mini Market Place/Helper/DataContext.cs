using Microsoft.EntityFrameworkCore;
using Mini_Market_Place.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Helper
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPurchaseHistory> ItemPurchaseHistories { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
    }
}
