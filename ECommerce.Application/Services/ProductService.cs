using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<Product> CreateProduct(CreateProductDto dto)
        {
            if (dto.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than zero.");
            }

            if (dto.StockQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative.");
            }
            var skuExists = await _productRepo.SKUExists(dto.SKU);
            if (skuExists)
            {
                throw new KeyNotFoundException($"Product with SKU '{dto.SKU}' already exists.");
            }

            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU.ToUpper(),
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };
            return await _productRepo.CreateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _productRepo.GetProductById(id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            await _productRepo.DeleteProduct(product);
        }

        public Task<List<Product>?> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }

        public Task<Product?> GetProductById(int id)
        {
            return _productRepo.GetProductById(id);
        }

        public async Task UpdateProduct(int id, Product product)
        {
            var existing = await _productRepo.GetProductById(id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            if (product.Price <= 0)
                throw new ArgumentException("Price must be positive.");

            existing.Name = product.Name;
            existing.SKU = product.SKU;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;

            await _productRepo.UpdateProduct(existing);
        }
    }
}
