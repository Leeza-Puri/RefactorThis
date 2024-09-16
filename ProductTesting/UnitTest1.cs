using Moq;
using NUnit.Framework;
using refactor_me.Models;
using refactor_me.Xero___Repository;
using refactor_this.Controllers;
using System;

namespace Tests
{

    [TestFixture]
    public class Tests
    {
         Mock<IProductRepository<Product>> prodRep;
        Mock<IProductConext> prodContext;
        ProductsController productsController;
        public Tests()
        {
            Setup();
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddNewProduct()
        {
            Guid id = new Guid();
            Product product = new Product();
            product.Name = "TestProduct";
            product.Description = "Test Product Desc";
            product.Price = Convert.ToDecimal(90.2);
            product.Id = id;

            //prodRep.Setup(r => r.Insert(new Product()));

         //   productsController.Create(product);

           // Assert.Pass();
        }
   }
}