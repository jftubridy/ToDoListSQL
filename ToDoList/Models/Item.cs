using System.Collections.Generic;
using System;

namespace ToDoList.Models
{
    public class Item
    {
        public Item()
        {
            this.Categories = new HashSet<CategoryItem>();
        }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public bool TaskComplete { get; set; }
        public DateTime Duedate { get; set; }

        public ICollection<CategoryItem> Categories { get; }


    }
}