using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorExe
{
    internal class Shelf
    {
        public const double SHELF_SPACE= 100;
        public static int UniqueId = 1;
        public int Id { get; }
        public int FloorNumber { get; set; }
        public double FreeSpace { get; set; }
        public List<Item> Items { get; set; }

        public Shelf(int floorNumber)
        {
            Id = UniqueId++;
            FloorNumber = floorNumber;
            FreeSpace = SHELF_SPACE;
            Items = new List<Item>();
        }

        public string ToString()
        {
            return $"\tShelf:\n\tId:{Id}\n\tFloor number:{FloorNumber}\n\tFree space:{FreeSpace} samar\n\tItems:\n{ReturnItems()}\n";
        }

        public string ReturnItems()
        {
            string items = "";
            foreach (Item item in Items)
            {
                items+=item.ToString();
            }
            return items;
        }

        
    }
}
