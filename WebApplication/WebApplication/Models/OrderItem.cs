﻿namespace WebApplication.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public Order Order { get; set; }
        public Item Item { get; set; }
    }
}
