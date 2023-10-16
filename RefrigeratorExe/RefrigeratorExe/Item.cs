using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorExe
{
    internal class Item
    {
        public static int UniqueId { get; set; } = 1;
        public int Id { get; }
        public string Name { get; set; }
        public int NumberShelf { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int TakeSpace { get; set; }
        public enum ItemType
        {
            Food,
            Drink
        }

        public enum KosherType
        {
            Fleshy,
            Milky,
            Fur
        }



        public Item()
        {
            Id = UniqueId++;
        }
    }
}
