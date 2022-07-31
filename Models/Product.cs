using System;
using System.Collections.Generic;

#nullable disable

namespace OrderFoodAPI.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal? Price { get; set; }
        public string Portions { get; set; }
    }
}
