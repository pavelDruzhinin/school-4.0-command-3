using Auctionator.Data;
using Auctionator.Models;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> Create(ProductDto productDto)
        {
            var newProduct = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Photos = productDto.Photos,
                Description = productDto.Description,
                ShortDescription = productDto.ShortDescription,
                Status = Enums.ProductStatus.WaitAuction,
                
            };

            await _db.Products.AddAsync(newProduct);
            await _db.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> Edit(int ProductId, ProductDto productDto)
        {
            var product = await _db.Products.FindAsync(ProductId);
            _db.Products.Attach(product);
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Photos = productDto.Photos;
            product.Description = productDto.Description;
            product.ShortDescription = productDto.ShortDescription;
            product.Status = productDto.Status;                
            _db.Products.Update(product);
            return product;
        }

        public async Task<Product> Delete(int ProductId)
        {
            var product = await _db.Products
                .Include(p => p.Auction)
                .Include(p => p.Buyer)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == ProductId);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return product;
        }

        public async Task<Product> Save(int ProductId, ProductDto productDto)
        {
            if (ProductId == 0)
            {
                await _db.Products.AddAsync(new Product());
            }
            else
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId);

                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Photos = productDto.Photos;
                product.Description = productDto.Description;
                product.ShortDescription = productDto.ShortDescription;
                product.Status = productDto.Status;
            }
            _db.SaveChanges();

            return await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId);
        }

        public async Task<Product> Details(int ProductId)
        {
            var product = await _db.Products
                .Include(p => p.Name)
                .Include(p => p.Price)
                .Include(p => p.Photos)
                .Include(p => p.Description)
                .Include(p => p.ShortDescription)
                .Include(p => p.Status)
                .Include(p => p.SubscribedProducts)
                .Include(p => p.AuctionId)
                .Include(p => p.OwnerId)
                .Include(p => p.BuyerId)                
                .FirstOrDefaultAsync(p => p.Id == ProductId);
            
            return product;
        }

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
