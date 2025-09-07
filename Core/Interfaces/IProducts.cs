using System;
using Core.Entites;

namespace Core.Interfaces;

public interface IProductsRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetProductByIdAsync(int id);
    void AddProducts(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExist(int id);
    Task<bool> SaveChangesAsync();




}
