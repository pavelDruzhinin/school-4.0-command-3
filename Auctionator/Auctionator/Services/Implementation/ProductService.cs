using Auctionator.Data;
using Auctionator.Models;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctionator.Enums;

namespace Auctionator.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddPhotos(IList<ProductPhoto> photos)
        {
            await _db.ProductPhotos.AddRangeAsync(photos);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> Create(ProductDto productDto, string ownerId)
        {
            var newProduct = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                ShortDescription = productDto.ShortDescription,
                Status = Enums.ProductStatus.WaitAuction,
                //OwnerId = productDto.OwnerId
            };

            await _db.Products.AddAsync(newProduct);
            await _db.SaveChangesAsync();

            return newProduct;
        }

        public async Task Edit(int productId, ProductDto productDto)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product != null)
            {
                _db.Products.Attach(product);
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Description = productDto.Description;
                product.ShortDescription = productDto.ShortDescription;             
                await _db.SaveChangesAsync();
            }
        }

        public async Task Delete(int productId)
        {
            var product = await _db.Products.
                Where(x => x.Status != ProductStatus.Deleted).
                FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                _db.Products.Attach(product);
                product.Status = Enums.ProductStatus.Deleted;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _db.Products            
                .FirstOrDefaultAsync(p => p.Id == productId);

            return product;
        }

        //TODO: проверить остальные методы!!!

        public async Task<SubscribedProduct> AddSubscription(SubscribedProductDto subscribedProductDto)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == subscribedProductDto.ProductId);
            var newSubscribedProduct = new SubscribedProduct()
            {
                UserId = subscribedProductDto.UserId,
                ProductId = subscribedProductDto.ProductId
            };
            await _db.SubscribedProducts.AddAsync(newSubscribedProduct);
            await _db.SaveChangesAsync();
            return newSubscribedProduct;
        }

        public async Task<List<Product>> GetAll(Enums.ProductStatus status)
        {
            return await _db.Products.Where(x => x.Status == status).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByOwner(string userId)
        {
            return await _db.Products.Where(x => x.OwnerId == userId).Include(x => x.Owner).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Product>> GetBoughtProducts(string userId)
        {
            return await _db.Products.Where(x => x.BuyerId == userId).Include(x => x.Buyer).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Product>> GetSaleProducts(string userId)
        {
            return await _db.Products.Where(x => x.OwnerId != userId).Where(x => x.Status == Enums.ProductStatus.WaitAuction || x.Status == Enums.ProductStatus.OnAuction).OrderByDescending(x => x.Id).ToListAsync();
        }                
    }
}
