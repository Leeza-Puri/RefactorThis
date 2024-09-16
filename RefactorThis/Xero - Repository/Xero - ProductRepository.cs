using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace refactor_me.Xero___Repository
{
    /// <summary>
    /// Respository to manage Product CRUD operations
    /// </summary>
    public class ProductRepository : IProductRepository<Product>
    {
        private IProductContext _context;

        public ProductRepository(IProductContext productContext)
        {
            this._context = productContext;
        }
        /// <summary>
        /// Delete the specefic Product.
        /// </summary>
        /// <param name="productID">GUID of the product for deletion</param>
        public void Delete(Guid productID)
        {
            var product = this._context.Product.Find(productID);
            this._context.Product.Remove(product);
        }

        /// <summary>
        /// Get the Product table againt product ID
        /// </summary>
        /// <param name="productId">Product Id of the Product</param>
        /// <returns></returns>
        public List<Product> Find(Guid productId)
        {
            List<Product> productList = new List<Product>();
            
            productList.Add(this._context.Product.Find(productId));
            return productList;
        }

        public List<Product> Find(string productName)
        {
           return this._context.Product.Where(c => c.Name == productName).ToList();
        }

        public List<Product> Get()
        {
            return _context.Product.ToList();
        }

        public void Insert(Product product)
        {
            _context.Product.Add(product);
        }

        /// <summary>
        /// Commit the Changes.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        public EntityState Update(Guid id, Product product)
        {
            var result = this._context.Product.SingleOrDefault(b => b.Id == id);
            if (result == null)
            {
                return EntityState.Added;
            }
            result.Id = product.Id;
            result.Name = product.Name;
            result.Price = product.Price;
            result.Description = product.Description;
            result.DeliveryPrice = product.Price;
            this._context.SaveChanges();
            return EntityState.Modified;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ProductRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}