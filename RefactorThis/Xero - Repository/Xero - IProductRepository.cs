using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Xero___Repository 
{
    /// <summary>
    /// Contract for Product Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProductRepository<T> : IDisposable
    {
        List<T> Get();
        List<T> Find(Guid productId);
        List<T> Find(string productName);

        void Insert(T product);
        void Delete(Guid productID);
        EntityState Update(Guid id, T product);

        void Save();
    }
}
