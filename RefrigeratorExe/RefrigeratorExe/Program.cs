using System;

namespace RefrigeratorExe
{
    internal class Program
    {
        #region Menu
        public static void Menu(Refrigerator refrigerator1, List<Refrigerator> refrigerators)
        {
            int inputNumber;
            do
            {
                inputNumber = InputCheck();
                switch (inputNumber)
                {
                    case 1:
                        {
                            Console.WriteLine(refrigerator1.ToString());
                            break;
                        }
                    case 2:
                        {
                            double space = refrigerator1.FreePlaceRefrigerator();
                            Console.WriteLine($"{space} Samar free space in the refrigerator");

                            break;
                        }
                    case 3:
                        {
                            Item newItem = CreateItem();
                            refrigerator1.InsertItem(newItem);
                            break;
                        }
                    case 4:
                        {
                            if (refrigerator1.Shelfs.Count() == 0)
                            {
                                Console.WriteLine("The refrigerator is empty!");
                            }
                            else
                            {
                                int idItemRemove = InputNumer();
                                Console.WriteLine(refrigerator1.RemoveItem(idItemRemove));
                            }
                            break;
                        }
                    case 5:
                        {
                            string allItem = refrigerator1.CleanRefrigerator();
                            Console.WriteLine("The refrigerator is clean!");
                            Console.WriteLine(allItem);
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("What do you want to eat?");
                            Item.ItemType typeItem = InputType();
                            Item.KosherType kosherType = InputKosher();
                            Console.WriteLine(refrigerator1.IWantEat(typeItem, kosherType));
                            break;
                        }
                    case 7:
                        {
                            SortItems(refrigerators);
                            break;
                        }
                    case 8:
                        {
                            SortShelfs(refrigerators);
                            break;
                        }
                    case 9:
                        {
                            SortRefrigerators(refrigerators);
                            break;
                        }
                    case 10:
                        {
                            refrigerator1.PreparingShopping();
                            break;
                        }
                    case 100:
                        {
                            Console.WriteLine("Bye bye...");
                            break;
                        }

                }
            } while (inputNumber != 100);
        }

        #endregion

        #region Menu-input check
        public static int InputCheck()
        {
            bool isParse = false;
            int inputNumber = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(
                "Press 1: the program will print all the items on the refrigerator and all its contents.\n" +
                "Press 2: the program will print how much space is left in the refrigerator \n" +
                "Press 3: the program will allow the user to put an item in the refrigerator. \n" +
                "Press 4: the program will allow the user to remove an item from the refrigerator. \n" +
                "Press 5: the program will clean the refrigerator and print to the user all the checked items. \n" +
                "Press 6: the program will ask the user 'What do I want to eat?' and bring the function to bring a product. \n" +
                "Perss 7: the program will print all the products arranged according to their expiration date. \n" +
                "Press 8: the program will print all the shelves arranged according to the free space left on them. \n" +
                "Press 9: the program will print all the refrigerators arranged according to the free space left in them. \n" +
                "Press 10: the program will prepare the refrigerator for shopping\n" +
                "Press 100: the close the system.");
                Console.ResetColor();
                isParse = int.TryParse(Console.ReadLine(), out inputNumber);
                if (isParse && ((inputNumber > 0 && inputNumber <= 10) || inputNumber == 100))
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();
            }

            while (!isParse || (inputNumber <= 0 || inputNumber > 10));
            return inputNumber;
        }
        #endregion

        #region Create item
        public static Item CreateItem()
        {
            string nameItem = InputName();
            Item.ItemType typeItem = InputType();
            Item.KosherType kosherType = InputKosher();
            DateOnly expiryDate = InputDate();
            double numberSpace = InputNumerSpace();
            Item item = new Item(nameItem, typeItem, kosherType, expiryDate, numberSpace);
            return item;
        }
        #endregion

        #region Input string name
        public static string InputName()
        {
            string nameItem = "";
            do
            {
                Console.WriteLine("Enter the name of the item");
                nameItem = Console.ReadLine();
                if (nameItem != "")
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();

            } while (nameItem == "");
            return nameItem;

        }
        #endregion

        #region Input type item
        public static Item.ItemType InputType()
        {
            bool isParse = false;
            int inputNumber = 0;
            Item.ItemType typeItem = Item.ItemType.Drink;
            do
            {
                Console.WriteLine("The item type: \nPress 1 for food \nPress 2 for drink");
                isParse = int.TryParse(Console.ReadLine(), out inputNumber);
                if (isParse && (inputNumber == 1 || inputNumber == 2))
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();
            }

            while (!isParse || (inputNumber != 1 || inputNumber != 2));
            switch (inputNumber)
            {
                case 1:
                    {
                        typeItem = Item.ItemType.Food;
                        break;
                    }
                case 2:
                    {
                        typeItem = Item.ItemType.Drink;
                        break;
                    }
            }
            return typeItem;
        }
        #endregion

        #region input kosher type
        public static Item.KosherType InputKosher()
        {
            bool isParse = false;
            int inputNumber = 0;
            Item.KosherType kosherType = Item.KosherType.Fleshy;

            do
            {
                Console.WriteLine("The item kosher: \nPress 1 for fleshy \nPress 2 for milky\nPress 3 for fur");
                isParse = int.TryParse(Console.ReadLine(), out inputNumber);
                if (isParse && (inputNumber > 0 && inputNumber < 4))
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();
            }

            while (!isParse || (inputNumber <= 0 || inputNumber >= 4));
            switch (inputNumber)
            {
                case 1:
                    {
                        kosherType = Item.KosherType.Fleshy;
                        break;
                    }
                case 2:
                    {
                        kosherType = Item.KosherType.Milky;
                        break;
                    }
                case 3:
                    {
                        kosherType = Item.KosherType.Fur;
                        break;
                    }

            }
            return kosherType;
        }
        #endregion

        #region Input int number
        public static int InputNumer()
        {
            bool isParse = false;
            int inputNumber = 0;
            do
            {
                Console.WriteLine("Enter the ID of the item you want to delete");
                isParse = int.TryParse(Console.ReadLine(), out inputNumber);
                if (isParse && inputNumber > 0)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();
            }

            while (!isParse || inputNumber <= 0);
            return inputNumber;
        }
        #endregion

        #region Input double number
        public static double InputNumerSpace()
        {
            bool isParse = false;
            double inputNumber = 0;
            do
            {
                Console.WriteLine("Enter item size");
                isParse = double.TryParse(Console.ReadLine(), out inputNumber);
                if (isParse && inputNumber > 0)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();
            }

            while (!isParse || inputNumber <= 0);
            return inputNumber;
        }
        #endregion

        #region Input expiry date
        public static DateOnly InputDate()
        {
            bool isParse = false;
            int day, month, year;
            do
            {
                Console.WriteLine("Enter the expiration date: \nEnter 3 numbers: The first for the day the second for the month and the third for the year! ");
                isParse = int.TryParse(Console.ReadLine(), out day);
                isParse = int.TryParse(Console.ReadLine(), out month);
                isParse = int.TryParse(Console.ReadLine(), out year);
                if (isParse && day > 0 && day <= 30 && month > 0 && month <= 12 && year >= 1000 && year <= 9999)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The value you entered is incorrect!\n");
                Console.ResetColor();
            }

            while (!isParse || ((day < 0 || day > 30) || (month < 0 || month > 12) || (year < 1000 || year > 9999)));
            DateOnly date = new DateOnly(year, month, day);
            return date;
        }
        #endregion

        #region Sort refrigerators
        public static void SortRefrigerators(List<Refrigerator> refrigerators)
        {
            if (refrigerators.Count() == 0)
            {
                Console.WriteLine("The refrigerator is empty!");
            }
            else
            {
                Console.WriteLine("Sort refrigerators:");
                refrigerators.Sort((refrigerator1, refrigerator2) => refrigerator1.FreePlaceRefrigerator().CompareTo(refrigerator2.FreePlaceRefrigerator()));
                refrigerators.Reverse();
                foreach (Refrigerator refrigerator in refrigerators)
                {
                    Console.WriteLine(refrigerator.ToString());
                }
            }
        }
        #endregion

        #region Sort shelfs
        public static void SortShelfs(List<Refrigerator> refrigerators)
        {

            if (refrigerators.Count() == 0)
            {
                Console.WriteLine("The refrigerator is empty!");
            }
            else
            {
                List<Shelf> shelfsSort = new List<Shelf>();
                int numberShelfEmpty = 0;
                foreach (Refrigerator refrigerator in refrigerators)
                {
                    numberShelfEmpty += (refrigerator.NumberShelfs - refrigerator.Shelfs.Count());
                    foreach (Shelf shelf in refrigerator.Shelfs)
                    {
                        shelfsSort.Add(shelf);
                    }
                }
                Console.WriteLine("Sort shelfs:");
                shelfsSort.Sort((shelf1, shelf2) => shelf1.FreeSpace.CompareTo(shelf2.FreeSpace));
                shelfsSort.Reverse();
                //I asked if I could write a number of empty shelfs instead of details of empty shelfs and I was told that I could because I add a shelf to the list of shelfs by item                if (numberShelfEmpty > 0)
                if (numberShelfEmpty > 0)
                {
                    Console.WriteLine($"There are {numberShelfEmpty} empty shelfs!!");
                }
                foreach (Shelf shelf in shelfsSort)
                {
                    Console.WriteLine(shelf.ToString());
                }
            }
        }
        #endregion

        #region Sort items
        public static void SortItems(List<Refrigerator> refrigerators)
        {
            if (refrigerators.First().Shelfs.Count() == 0 && refrigerators.Count() > 0)
            {
                Console.WriteLine("The refrigerator is empty!");
            }
            else
            {
                List<Item> itemsSort = new List<Item>();
                foreach (Refrigerator refrigerator in refrigerators)
                {
                    foreach (Shelf shelf in refrigerator.Shelfs)
                    {
                        foreach (Item item in shelf.Items)
                        {
                            itemsSort.Add(item);
                        }
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

        static void Main(string[] args)
        {
            List<Refrigerator> refrigerators = new List<Refrigerator>();
            Refrigerator refrigerator1 = new Refrigerator("Sharp", "gray", 2);
            refrigerators.Add(refrigerator1);
            Console.WriteLine("Hello User!");
            Menu(refrigerator1, refrigerators);
        }

    }
}
