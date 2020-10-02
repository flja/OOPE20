using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject
{
    class Menu
    {
        string _title;

        List<MenuItem> menuItems = new List<MenuItem>();

        public Menu(string title)
        {
            _title = title;
        }

        public void Add(MenuItem item)
        {
            menuItems.Add(item);
        }

        public void Start()
        {
            Console.Clear();
            Console.SetCursorPosition(10, 0);
            Console.WriteLine($"[{_title}]");

            foreach(MenuItem item in menuItems)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{item.Title}");
            }
            Console.SetCursorPosition(0, 1);
            SelectMenuItem(0);
        }

        public void HandleKeyEvent(ConsoleKeyInfo key)
        {
            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    {
                        MoveUp();
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        MoveDown();
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        //menuItems[_currPos].Select();
                        break;
                    }
            }
        }

        void SelectMenuItem(int itemPos)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{menuItems[Console.CursorTop - 1].Title}");
            
            Console.SetCursorPosition(0, itemPos + 1);

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{menuItems[itemPos].Title}");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        void MoveUp()
        {
            int pos = Console.CursorTop--;
            if (pos < 0)
                pos = menuItems.Count - 1;
            SelectMenuItem(pos);
        }

        void MoveDown()
        {
            int pos = (Console.CursorTop++) % menuItems.Count;
            SelectMenuItem(pos);
        }
    }
}
