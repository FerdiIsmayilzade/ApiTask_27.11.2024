using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Exceptions;
using Service.Helpers.DTOs.Categories;
using Service.Helpers.DTOs.Products;
using Service.Services;
using Service.Services.Interfaces;

namespace Onion_Api.Controllers.Admin
{
   
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var datas = await _productService.GetAllAsync();
            return Ok(datas);
        }

        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(await _productService.GetByIdAsync(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [ProducesResponseType(typeof(ProductCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProductCreateDto), StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            await _productService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Successfully Created");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            var datas = await _productService.SearchAsync(searchText);
            return Ok(datas);
        }

        [ProducesResponseType(typeof(ProductEditDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductEditDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductEditDto), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ProductEditDto request)
        {
            try
            {
                await _productService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
