﻿using System.ComponentModel.DataAnnotations;

namespace MacroCenter.Models
{
    /// <summary>
    /// Class that holds information about Products. Adding model validation
    /// in the form of attribute tags/decorations in task 56.
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a product description")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }
        public string ImageString { get; set; }
    }
}