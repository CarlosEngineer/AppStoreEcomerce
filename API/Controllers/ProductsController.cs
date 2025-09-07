using System;
using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace API.Controllers;

[ApiController]
[Route("api/Products")]
public class ProductsController(IProductsRepository _repo) : ControllerBase
{
    

    [HttpGet]

    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        return Ok(await _repo.GetAllAsync());
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _repo.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _repo.AddProducts(product);

        if (await _repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id },product);
        }
        return BadRequest("Problem create product");
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {

        if (product.Id != id || !ProductExist(id))
            return BadRequest("Cannot update this product");

        _repo.UpdateProduct(product);
       
        if (await _repo.SaveChangesAsync())
        {
            return NoContent();
        }
        

        return BadRequest("Problem updating product");
    }

    [HttpDelete("{id:int}")]

    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await _repo.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _repo.DeleteProduct(product);

        if (await _repo.SaveChangesAsync())
        {
            return NoContent();
        }
    
        return NoContent();    
    }

    private bool ProductExist(int id)
    {
        return _repo.ProductExist(id);
    }

}
