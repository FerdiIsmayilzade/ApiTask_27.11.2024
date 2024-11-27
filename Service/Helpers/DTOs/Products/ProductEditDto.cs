using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Products
{
    public class ProductEditDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<IFormFile> Files{ get; set; }

        public int CategoryId { get; set; }
        public List<int> DiscountIds { get; set; }
        public List<int> ColorIds { get; set; }
    }
}
