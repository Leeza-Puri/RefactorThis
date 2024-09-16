using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace refactor_me.Xero___Repository
{
    /// <summary>
    /// Respository to manage Product Options CRUD operations
    /// </summary>
    public class ProductOptionsRepository : IProductRepository<ProductOptions>
    {
        private IProductContext _context;

        public ProductOptionsRepository(IProductContext productContext)
        {
            this._context = productContext;
        }

        public void Delete(Guid productID)
        {
            var product = this._context.ProductOption.Find(productID);
            this._context.ProductOption.Remove(product);
        }

        /// <summary>
        /// Get the Product Options against the Product ID
        /// </summary>
        /// <param name="productId">GUID of the Product.</param>
        /// <returns></returns>
        public List<ProductOptions> Find(Guid productId)
        {
            return this._context.ProductOption.Where(c => c.ProductId == productId).ToList();

        }

        /// <summary>
        /// Find the Product Options against the Product Name
        /// </summary>
        /// <param name="productName">Name of the Product Options</param>
        /// <returns></returns>
        public List<ProductOptions> Find(string productName)
        {
           return this._context.ProductOption.Where(c => c.Name == productName).ToList();
        }

        /// <summary>
        /// Get all the Product Options
        /// </summary>
        /// <returns></returns>
        public List<ProductOptions> Get()
        {
            return _context.ProductOption.ToList();
        }

        /// <summary>
        /// Addd new Product Options
        /// </summary>
        /// <param name="product"></param>
        public void Insert(ProductOptions product)
        {
            _context.ProductOption.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Update the Product options against the Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productOptions"></param>
        /// <returns></returns>
        public EntityState Update(Guid id, ProductOptions productOptions)
        {
            var result = this._context.ProductOption.SingleOrDefault(b => b.Id == id);
            // assume Entity base class have an Id property for all items
            //var entity = Find(id);
            if (result == null)
            {
                return EntityState.Added;
            }
            result.Id = productOptions.Id;
            result.Name = productOptions.Name;
            result.Description = productOptions.Description;
            result.ProductId = productOptions.ProductId;
            
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