using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            // var product = new Product
            // {
            //     ProductName = productDto.ProductName,
            //     Price = productDto.Price,
            //     CategoryId = productDto.CategoryId
            // };
            Product product = _mapper.Map<Product>(productDto);
            _manager.ProductRepository.CreateOneProduct(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            var product = _manager.ProductRepository.GetOneProduct(id, true);
            if (product != null)
            {
                _manager.ProductRepository.DeleteOneProduct(product);
                _manager.Save();
            }
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.ProductRepository.GetAllProducts(trackChanges);
        }

        public Product GetOneProduct(int id, bool trackChanges)
        {
            Product? product = _manager.ProductRepository.GetOneProduct(id, trackChanges);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product = GetOneProduct(id, trackChanges);
            ProductDtoForUpdate productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }

        public void UpdateOneProduct(ProductDtoForUpdate product)
        {
            // var entity = _manager.ProductRepository.GetOneProduct(product.ProductId, true);
            // entity = _mapper.Map<Product>(product);

            // entity.ProductName = product.ProductName;
            // entity.Price = product.Price;
            // entity.CategoryId = product.CategoryId;

            // Burada mapper ile yeni bir nesne üretildiği için GetOneProduct da olan izleme aktif olmayacak o yüzden UpdateOneProduct ile güncelliyoruz

            var entity = _mapper.Map<Product>(product);
            _manager.ProductRepository.UpdateOneProduct(entity);
            _manager.Save();
        }
    }
}