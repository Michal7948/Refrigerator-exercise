using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorExe
{
    internal class Item
    {
        public static int UniqueId= 1;
        public int Id { get; }
        public string Name { get; set; }
        public int NumberShelf { get; set; }
        public ItemType Type { get; set; }
        public KosherType Kosher { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public double TakeSpace { get; set; }
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

        public Item(string name,ItemType type,KosherType kosher, DateOnly expiryDate,double takeSpace)
        {
            Id = UniqueId++;
            Name = name;
            Type = type;
            Kosher = kosher;
            ExpiryDate = expiryDate;
            TakeSpace = takeSpace;
        }

        public string ToString()
        {
            return $"\t\tItem:\n\t\tId:{Id}\n\t\tName:{Name}\n\t\tNumber shelf:{NumberShelf}\n\t\tType:{Type}\n\t\tKosher:{Kosher}\n\t\tExpiry Date:{ExpiryDate}\n\t\tTake space:{TakeSpace} samar\n\n";
        }
    }
}
