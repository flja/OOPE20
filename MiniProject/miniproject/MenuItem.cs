using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject
{
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
