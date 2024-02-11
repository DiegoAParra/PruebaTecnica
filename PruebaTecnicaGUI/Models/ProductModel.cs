using System;
using System.Collections.Generic;

namespace PruebaTecnicaGUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}