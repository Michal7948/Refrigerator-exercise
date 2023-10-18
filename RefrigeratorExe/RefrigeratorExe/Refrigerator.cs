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

        #region Free place refrigerator
        public double FreePlaceRefrigerator()
        {
            double freePlace = 0;
            foreach (Shelf shelf in Shelfs)
            {
                freePlace += shelf.FreeSpace;
            }
            return freePlace += (NumberShelfs - Shelfs.Count()) * Shelf.SHELF_SPACE;
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
            string allItems = "";
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items.ToList<Item>())
                {
                    allItems += item.ToString();
                    if (item.ExpiryDate < DateOnly.FromDateTime(DateTime.Now))
                    {
                        shelf.Items.Remove(item);
                        shelf.FreeSpace += item.TakeSpace;
                        Console.WriteLine($"The item {item.Name} has expired,\nHe was thrown in the trash");
                    }
                }
            }
            Console.WriteLine("The refrigerator is clean!");
            return allItems;
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
            

            if (FreePlaceRefrigerator() >= 20)
            {
                Console.WriteLine("Excellent!\nYou can go shopping...");
            }
            else
            {
                CleanRefrigerator();
                double freePlaceRefrigerator = FreePlaceRefrigerator();
                if (freePlaceRefrigerator >= 20)
                {
                    Console.WriteLine("Excellent!\nYou can go shopping...");
                }
                else
                {
                    DateOnly dateMilky = DateOnly.FromDateTime(DateTime.Now).AddDays(3);
                    freePlaceRefrigerator += CheckExpiry(dateMilky);
                    //check Milky
                    if (freePlaceRefrigerator >= 20)
                    {
                        RemoveItemExpiry(dateMilky);
                        Console.WriteLine("Excellent!\nYou can go shopping...");
                    }
                    //check Fleshy
                    else
                    {
                        DateOnly dateFleshy = DateOnly.FromDateTime(DateTime.Now).AddDays(7);
                        freePlaceRefrigerator += CheckExpiry(dateFleshy);
                        if (freePlaceRefrigerator >= 20)
                        {
                            RemoveItemExpiry(dateMilky);
                            RemoveItemExpiry(dateFleshy);
                            Console.WriteLine("Excellent!\nYou can go shopping...");
                        }
                        //check Fur
                        else
                        {
                            DateOnly dateFur = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
                            freePlaceRefrigerator += CheckExpiry(dateFur);
                            if (freePlaceRefrigerator >= 20)
                            {
                                RemoveItemExpiry(dateMilky);
                                RemoveItemExpiry(dateFleshy);
                                RemoveItemExpiry(dateFur);
                                Console.WriteLine("Excellent!\nYou can go shopping...");
                            }
                            else Console.WriteLine("It is not Time to shop!!!");
                        }
                    }

                }

            }
        }

        public double CheckExpiry(DateOnly date)
        {
            double takePlace = 0;
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.ExpiryDate < date)
                    {
                        takePlace += item.TakeSpace;
                    }
                }
            }
            return takePlace;
        }

        public void RemoveItemExpiry(DateOnly date)
        {
            double takePlace = 0;
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.ExpiryDate < date)
                    {
                        shelf.Items.Remove(item);
                        shelf.FreeSpace += item.TakeSpace;
                        Console.WriteLine($"The {item.Name} item expires in the next few days,\n He was thrown in the trash");
                    }
                }
            }
        }

        public void CheckAndRemove()
        {

        }
        #endregion

        #region Sort items
        public void SortItems()
        {
            
            if (Shelfs.Count() == 0)
            {
                Console.WriteLine("The refrigerator is empty!");
            }
            else
            {
                List<Item> itemsSort = new List<Item>();
                foreach (Shelf shelf in Shelfs)
                {
                    foreach (Item item in shelf.Items)
                    {
                        itemsSort.Add(item);
                    }
                }
                Console.WriteLine("Sort items:");
                itemsSort.Sort((item1, item2) => item1.ExpiryDate.CompareTo(item2.ExpiryDate));
                foreach (Item item in itemsSort)
                {
                    Console.WriteLine(item.ToString());
                }
                
            }
        }
        #endregion

        #region Sort shelfs
        public void SortShelfs()
        {
            if (Shelfs.Count() == 0)
            {
                Console.WriteLine("The refrigerator is empty!");
            }
            else
            {
                Console.WriteLine("Sort shelfs:");
                Shelfs.Sort((shelf1, shelf2) => shelf1.FreeSpace.CompareTo(shelf2.FreeSpace));
                foreach (Shelf shelf in Shelfs)
                {
                    Console.WriteLine(shelf.ToString());
                }
            }
        }
        #endregion

        #region Print only items
        public void PrintItem()
        {
            if (Shelfs.Count() == 0)
            {
                Console.WriteLine("The refrigerator is empty!");
            }
            foreach (Shelf shelf in Shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        #endregion
    }
}
