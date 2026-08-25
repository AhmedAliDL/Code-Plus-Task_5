using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IProductRepo
    {
        Task<List<Product>?> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<bool> SKUExists(string sku);
        Task RestoreStockAsync(
            int productId,
            int quantity);
    }
}
