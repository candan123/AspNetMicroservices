using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
           await _context.products.InsertOneAsync(product);

        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _context
                                    .products
                                    .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context
                          .products
                          .Find(p => true)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCatogery(string catogery)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, catogery);
            return await _context
                           .products
                           .Find(filter)
                           .ToListAsync();
        }

        public async Task<Product> GetProductByID(string id)
        {
            return await _context
                           .products
                           .Find(p => p.Id ==id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context
                           .products
                           .Find(filter)
                           .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                     .products
                                     .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;

        }
    } 
}
