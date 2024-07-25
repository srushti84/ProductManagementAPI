﻿using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Entities;

namespace ProductManagementAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            // Retrieve the existing product from the database
            var existingProduct = _context.Products.Find(product.Id);

            if (existingProduct != null)
            {
                // Update the existing product's properties
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
            }
            else
            {
                throw new InvalidOperationException($"Product with Id {product.Id} does not exist.");
            }

            // Save changes to the database
            _context.SaveChanges();
        }


        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
