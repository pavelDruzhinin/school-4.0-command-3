using Auctionator.Models;
using Auctionator.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auctionator.Services.Interface
{
    public interface IProductService
    {
        Task<Product> Create(ProductDto productDto);
        Task<Product> Edit(int ProductId, ProductDto productDto);
        Task<Product> Delete(int ProductId);
        Task<Product> Save(int ProductId, ProductDto productDto);
        Task<Product> Details(int ProductId);
        Task<SubscribedProduct> AddSubscription(SubscribedProductDto subscribedProductDto);
        Task<List<Product>> GetAll(Enums.ProductStatus status);
        Task<List<Product>> GetProductsByOwner(string userId);
        Task<List<Product>> GetBoughtProducts(string userId);
        Task<List<Product>> GetSaleProducts(string userId);        
    }
}
