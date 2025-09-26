﻿namespace ShopApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string MainImg { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Traffic { get; set; }
        public int Rate { get; set; }
        
        public string Year { get; set; }  = string.Empty;
        public double Discount { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }
        
        //Author
        public int AuthorId { get; set; }

        public Category? Category { get; set; }
        public Publisher? Publisher { get; set; }
        public Author? Author { get; set; }

    }
}
