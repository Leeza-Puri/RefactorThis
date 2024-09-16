using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using refactor_me.Models;
using refactor_me.Xero___Repository;


namespace Tests
{
    class TestProductDbSet : TestDbSet<Product>
    {
        public override Product Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (Guid)keyValues.Single());
        }
    }

    class TestProductOptionsDbSet : TestDbSet<ProductOptions>
    {
        public override ProductOptions Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (Guid)keyValues.Single());
        }
    }
    public class TestProductAppContext : DbContext, IProductContext
    {
        public TestProductAppContext()
        {
            this.Product = new TestProductDbSet();
            this.ProductOption = new TestProductOptionsDbSet();
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductOptions> ProductOption { get; set; }

        public int SaveChanges()
       {
            return 0;
        }

        public void MarkAsModified(Product item) { }
        public void Dispose() { }

        void IProductContext.SaveChanges()
        {
            
            this.SaveChanges();
        }

        DbEntityEntry IProductContext.Entry(object entity)
        {
            return base.Entry(entity);
        }
    }
}