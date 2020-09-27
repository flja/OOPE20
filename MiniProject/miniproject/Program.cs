using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace miniproject
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu("fancymenu");

            menu.Add(new MenuItem("Punkt1", "Indhold af punkt 1... det er indtil videre bare tekst"));
           
            menu.Add(new MenuItem("Punkt2", "Indhold af punkt 2... det er indtil videre også bare tekst"));
           
            menu.Add(new MenuItem("Et lidt længere menupunkt", "Indhold af punkt 3... det er indtil videre også bare tekst"));
           

            menu.Start();
            Console.ReadKey();
        }
    }

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
                Console.WriteLine($"{item.Title}");
            }
        } 
    }

    class MenuItem
    {
        public string Content { get; set; }
        public string Title { get; set; }

        public MenuItem(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public void Select()
        {

        }
    }

}
