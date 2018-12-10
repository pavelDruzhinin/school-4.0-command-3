using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Auctionator.Hubs;
using Auctionator.Models;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Auctionator.Controllers
{
    [Route("product")] // TODO: не забыть изменить ссылки в представлениях!!!!
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHostingEnvironment _appEnvironment;

        public ProductsController(IProductService productService, IHostingEnvironment appEnvironment)
        {
            _productService = productService;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        [Route("upload-img/{productId:int}")]
        public async Task<JsonResult> AddPhotos(IFormFileCollection uploads, int productId)
        {
            try
            {
                IList<ProductPhoto> photos = new List<ProductPhoto>();
                foreach (var uploadedFile in uploads)
                {
                    // путь к папке с изображениями
                    string path = "/images/products/" + productId + "/" + uploadedFile.FileName;
                    // сохраняем файл в папку в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    ProductPhoto photo = new ProductPhoto { ProductId = productId, Path = path };
                    photos.Add(photo);
                }
                await _productService.AddPhotos(photos);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }

        }

        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> Create([FromBody]ProductDto productDto)
        {
            try
            {
                //productDto.OwnerId = User.Identity.Name; //TODO: раскомментировать!!!
                var prod = await _productService.Create(productDto);
                return Json(new { success = true, result = prod.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [HttpPost]
        [Route("edit/{id:int}")]
        public async Task<JsonResult> Edit([FromBody] ProductDto productDto, int id)
        {
            try
            {
                await _productService.Edit(id, productDto);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [Route("delete/{id:int}")]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _productService.Delete(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [Route("details/{id:int}")]
        public async Task<JsonResult> Details(int id)
        {
            try
            {
                var product = await _productService.Details(id);
                return Json(new { success = true, result = product });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        //TODO: проверить остальные методы

        [HttpPost]
        [Route("addsubscription/{productId:int}")]
        public async Task<JsonResult> AddSubcription(string subscribedProduct)
        {
            SubscribedProductDto subscribedProductDto = JsonConvert.DeserializeObject<SubscribedProductDto>(subscribedProduct);

            try
            {
                var subscrib = await _productService.AddSubscription(subscribedProductDto);
                return Json(new { success = true, result = subscrib });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [Route("getall")]
        public async Task<JsonResult> GetProducts(Enums.ProductStatus status)
        {
            try
            {
                var products = await _productService.GetAll(status);
                return Json(new { success = true, result = products });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [Route("myownproducts/{id:int}")]
        public async Task<JsonResult> GetProductsByOwner(string userId)
        {
            try
            {
                var products = await _productService.GetProductsByOwner(userId);
                return Json(new { success = true, result = products });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [Route("myboughtproducts/{id:int}")]
        public async Task<JsonResult> GetBoughtProducts(string userId)
        {
            try
            {
                var products = await _productService.GetBoughtProducts(userId);
                return Json(new { success = true, result = products });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [Route("saleproducts/{id:int}")]
        public async Task<JsonResult> GetSaleProducts(string userId)
        {
            try
            {
                var products = await _productService.GetSaleProducts(userId);
                return Json(new { success = true, result = products });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /*
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Auction)
                .Include(p => p.Buyer)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Id");
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ShortDescription,Status,AuctionId,OwnerId,BuyerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Id", product.AuctionId);
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", product.BuyerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", product.OwnerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Id", product.AuctionId);
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", product.BuyerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", product.OwnerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ShortDescription,Status,AuctionId,OwnerId,BuyerId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Id", product.AuctionId);
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", product.BuyerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", product.OwnerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Auction)
                .Include(p => p.Buyer)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        */
    }
}
