﻿using System;

namespace SunnyBuy.Services.CartServices.Models
{
    public class ListModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateInclude { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
