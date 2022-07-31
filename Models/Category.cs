using System;
using System.Collections.Generic;

#nullable disable

namespace OrderFoodAPI.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
