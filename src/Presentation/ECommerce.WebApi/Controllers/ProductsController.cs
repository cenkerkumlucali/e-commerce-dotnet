using System.Net;
using ECommerce.Application.Abstractions.Storage;
using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.File;
using ECommerce.Application.Repositories.InvoiceFile;
using ECommerce.Application.Repositories.ProductImageFile;
using ECommerce.Application.RequestParameters;
using ECommerce.Application.ViewModels.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductWriteRepository _productWriteRepository;
        private IProductReadRepository _productReadRepository;
        private IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageReadRepository _productImageFileReadRepository;
        readonly IProductImageWriteRepository _productImageFileWriteRepository;
        readonly IInvoiceReadRepository _invoiceFileReadRepository;
        readonly IInvoiceWriteRepository _invoiceFileWriteRepository;
        private IStorageService _storageService;

        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileReadRepository fileReadRepository,
            IProductImageReadRepository productImageFileReadRepository,
            IProductImageWriteRepository productImageFileWriteRepository,
            IInvoiceReadRepository invoiceFileReadRepository,
            IInvoiceWriteRepository invoiceFileWriteRepository,
            IFileWriteRepository fileWriteRepository,
            IStorageService storageService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _fileWriteRepository = fileWriteRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size).Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Stock,
                    c.Price,
                    c.CreatedDate,
                    c.UpdatedDate
                });
            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProduct createProduct)
        {
            await _productWriteRepository.AddAsync(new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                Stock = createProduct.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProduct updateProductDto)
        {
            Product product = await _productReadRepository.GetByIdAsync(updateProductDto.Id);
            product.Stock = updateProductDto.Stock;
            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result =
                await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() 
                {
                    product
                }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }
    }
}