using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorExe
{
    internal class Shelf
    {
        public static int UniqueId { get; set; } = 1;
        public int Id { get; }
        public int FloorNumber { get; set; }
        public int FreeSpace { get; set; }
        public List<Item> Items { get; set; }

        public Shelf()
        {
            Id = UniqueId++;
        }
    }
}
