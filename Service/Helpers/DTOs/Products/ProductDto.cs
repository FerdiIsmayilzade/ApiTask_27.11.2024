﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public string CategoryName { get; set; }
        public List<string> Images { get; set; }
        public List<string> ColorNames { get; set; }
        public List<int> Discounts { get; set; }
    }
}