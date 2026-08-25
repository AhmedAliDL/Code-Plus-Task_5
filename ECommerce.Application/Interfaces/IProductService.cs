using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>?> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product> CreateProduct(CreateProductDto dto);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);

    }
}
