using Mini_Market_Place.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Entities
{
    public class ItemPurchaseHistory
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public int Count { get; set; }

        public static implicit operator ItemPurchaseHistory(ItemHistoryViewModel model)
        {
            return model == null ? null : new ItemPurchaseHistory
            {
                Count = model.Count,
            };
        }
    }
}
