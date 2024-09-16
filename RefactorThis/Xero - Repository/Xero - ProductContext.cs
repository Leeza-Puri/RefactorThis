using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.EntityClient;
using System.Data.Entity;
using refactor_me.Models;
using System.Data.Entity.Infrastructure;
using System.Configuration;

namespace refactor_me.Xero___Repository
{
    public interface IProductContext
    {
        DbSet<Product> Product { get; set; }
        DbSet<ProductOptions> ProductOption { get; set; }

        void SaveChanges();
        DbEntityEntry Entry(object entity);
    }
    public class ProductContext : DbContext, IProductContext
    {
        // private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        private static string ConnectionString = ConfigurationManager.AppSettings["DBConnection"];

        public ProductContext() : base(ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data")))
        {       
        }

        /// <summary>
        /// Product Table
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// Product Options table
        /// </summary>
        public DbSet<ProductOptions> ProductOption { get; set; }

        void IProductContext.SaveChanges()
        {
            base.SaveChanges();
        }
        DbEntityEntry IProductContext.Entry(object entity)
        {
            return base.Entry(entity);
        }
    }
}