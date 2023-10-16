using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorExe
{
    internal class Refrigerator
    {
        public static int UniqueId { get; set; } = 1;
        public int Id { get; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberShelfs { get; set; }
        public List<Shelf> Shelfs { get; set; }
        public Refrigerator()
        {
            Id = UniqueId++;

        }
    }
}
