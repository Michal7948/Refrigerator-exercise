using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RefrigeratorExe
{
    internal class Refrigerator
    {
        public static int uniqueId = 1;
        public int Id { get; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberShelfs { get; set; }
        public List<Shelf> Shelfs { get; set; }


        public Refrigerator(string model, string color, int numberShelfs)
        {
            Id = uniqueId++;
            Model = model;
            Color = color;
            NumberShelfs = numberShelfs;
            Shelfs = new List<Shelf>();
        }


        #region ToString refrigerator
        public string ToString()
        {
            return $"Refrigerator:\nId:{Id}\nModel:{Model}\nColor:{Color}\nNumber shelfs:{NumberShelfs}\nShelfs:\n{ReturnShelfs()}\n";
        }

        public string ReturnShelfs()
        {
            string shelfs = "";
            if (Shelfs.Count() == 0)
            {
                return "The refrigerator is empty!";
            }
            foreach (Shelf shelf in Shelfs)
            {
                shelfs += shelf.ToString();
            }
            return shelfs;
        }
        #endregion

        #region Free place refrigerator
        public double FreePlaceRefrigerator()
        {
            double freePlace = 0;
            foreach (Shelf shelf in Shelfs)
            {
                freePlace += shelf.FreeSpace;
            }
            return freePlace += ((NumberShelfs - Shelfs.Count()) * Shelf.SHELF_SPACE);
        }
        #endregion

        #region Insert item
        public void InsertItem(Item item)
        {
            bool isShelfFree = false;
            foreach (Shelf shelf in Shelfs)
            {
                if (shelf.FreeSpace >= item.TakeSpace)
                {
                    isShelfFree = true;
                    item.NumberShelf = shelf.FloorNumber;
                    shelf.Items.Add(item);
                    shelf.FreeSpace -= item.TakeSpace;
                    Console.WriteLine($"The item was placed in the refrigerator successfully!!!");
                    return;
                }
            }
            if (isShelfFree == false && Shelfs.Count() < NumberShelfs)
            {
                Shelf shelfFree = new Shelf(Shelfs.Count() + 1);
                item.NumberShelf = shelfFree.FloorNumber;
                shelfFree.Items.Add(item);
                shelfFree.FreeSpace -= item.TakeSpace;
                Shelfs.Add(shelfFree);
                Console.WriteLine($"The item was placed in the refrigerator successfully!!!");
            }
            else Console.WriteLine("There is no place for this item in the refrigerator. \nPlease empty and try again!!!");
        }
        #endregion

        #region Remove item
        public string RemoveItem(int idItem)
        {
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Id == idItem)
                    {
                        shelf.Items.Remove(item);
                        shelf.FreeSpace += item.TakeSpace;
                        return item.ToString();
                    }
                }
            }
            return "The item is not in the refrigerator!!!";
        }
        #endregion

        #region Cleaning refrigerator
        public string CleanRefrigerator()
        {
            if (Shelfs.Count() == 0)
            {
               return "";
            }

            string allItem = $"All items before cleaning:\n";
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items.ToList())
                {
                    allItem += item.ToString();
                    if (item.ExpiryDate < DateOnly.FromDateTime(DateTime.Now))
                    {
                        shelf.Items.Remove(item);
                        shelf.FreeSpace += item.TakeSpace;
                        Console.WriteLine($"The item {item.Name} has expired, He was thrown in the trash");
                    }
                }
            }
            return allItem;
        }
        #endregion

        #region What do I want to eat?
        public string IWantEat(Item.ItemType type, Item.KosherType kosher)
        {
            string itemsWant = "";
            if (Shelfs.Count() == 0)
            {
                return "The refrigerator is empty!";
            }
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Type == type && item.Kosher == kosher && item.ExpiryDate >= DateOnly.FromDateTime(DateTime.Now))
                    {
                        itemsWant += $"{item.Name},\n";
                    }
                }
            }
            if (itemsWant == "")
            {
                return "There are no existing items in the refrigerator for these settings!";
            }
            return itemsWant;
        }
        #endregion

        #region Preparing for shopping
        public void PreparingShopping()
        {
            double freePlaceRefrigerator = FreePlaceRefrigerator();
            
            if (freePlaceRefrigerator >= 20)
            {
                Console.WriteLine("Excellent!\nYou can go shopping...");
            }
            else
            {
                CleanRefrigerator();
                freePlaceRefrigerator = FreePlaceRefrigerator();
                if (freePlaceRefrigerator >= 20) Console.WriteLine("Excellent!\nYou can go shopping...");
                else KosherCheckAndRemove(freePlaceRefrigerator);
            }
        }

        public void KosherCheckAndRemove(double freePlaceRefrigerator)
        {
            //check Milky
            DateOnly dateMilky = DateOnly.FromDateTime(DateTime.Now).AddDays(3);
            Item.KosherType kosherMilky = Item.KosherType.Milky;
            freePlaceRefrigerator += CheckExpiry(dateMilky, kosherMilky);
            if (freePlaceRefrigerator >= 20)
            {
                RemoveItemExpiry(dateMilky, kosherMilky);
                Console.WriteLine("Excellent!\nYou can go shopping...");
            }
            
            else
            {
                //check Fleshy
                DateOnly dateFleshy = DateOnly.FromDateTime(DateTime.Now).AddDays(7);
                Item.KosherType kosherFleshy = Item.KosherType.Fleshy;
                freePlaceRefrigerator += CheckExpiry(dateFleshy, kosherFleshy);
                if (freePlaceRefrigerator >= 20)
                {
                    RemoveItemExpiry(dateMilky, kosherMilky);
                    RemoveItemExpiry(dateFleshy, kosherFleshy);
                    Console.WriteLine("Excellent!\nYou can go shopping...");
                }
                //check Fur
                else
                {
                    DateOnly dateFur = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
                    Item.KosherType kosherFur = Item.KosherType.Fur;
                    freePlaceRefrigerator += CheckExpiry(dateFur, kosherFur);
                    if (freePlaceRefrigerator >= 20)
                    {
                        RemoveItemExpiry(dateMilky, kosherMilky);
                        RemoveItemExpiry(dateFleshy, kosherFleshy);
                        RemoveItemExpiry(dateFur, kosherFur);
                        Console.WriteLine("Excellent!\nYou can go shopping...");
                    }
                    else Console.WriteLine("It is not Time to shop!!!");
                }
            }
        }

        public double CheckExpiry(DateOnly date, Item.KosherType kosherType)
        {
            double takePlace = 0;
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.ExpiryDate < date && item.Kosher==kosherType)
                    {
                        takePlace += item.TakeSpace;
                    }
                }
            }
            return takePlace;
        }

        public void RemoveItemExpiry(DateOnly date, Item.KosherType kosherType)
        {
            double takePlace = 0;
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items.ToList())
                {
                    if (item.ExpiryDate < date && item.Kosher == kosherType)
                    {
                        shelf.Items.Remove(item);
                        shelf.FreeSpace += item.TakeSpace;
                        Console.WriteLine($"The {item.Name} item expires in the next few days, He was thrown in the trash");
                    }
                }
            }
        }

        public void CheckAndRemove()
        {

        }
        #endregion

    }
}
