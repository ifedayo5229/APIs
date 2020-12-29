using Mini_Market_Place.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int NoOfAvailableItems { get; set; }

        public Guid ItemCategoryId { get; set; }
        public ItemCategory Category { get; set; }

        public static implicit operator Item(ItemViewModel model)
        {
            return model == null ? null : new Item
            {
                Name = model.Name,
                Price = double.Parse(model.Price),
                Description = model.Description
            };
        }

        public static implicit operator Item(CreateItemViewModel model)
        {
            return model == null ? null : new Item
            {
                Name = model.Name,
                Price = double.Parse(model.Price),
                Description = model.Description,
                Category = model.Category,
                NoOfAvailableItems = model.NoOfAvailableItems
            };
        }
    }
}
