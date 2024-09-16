using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using refactor_me.Controllers;
//using NUnit.Framework;
using refactor_me.Models;
using refactor_me.Xero___Repository;
using refactor_this.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Results;

namespace Tests
{
    [TestClass]
    public class TestProductController
    {
        static Product dummyProduct;
        static ProductRepository TestRep;
        static ProductsController TestCont;
        static TestProductAppContext TestProdConext;

        static ProductOptions dummyProductOptions;
        static ProductOptionsRepository TestRepOptions;
        static ProductOptionsController TestContOptions;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            TestProdConext = new TestProductAppContext();

            SetupDemoProduct();
            TestRep = new ProductRepository(TestProdConext);
            TestCont = new ProductsController(TestRep);

            SetupDemoProductOptions();
            TestRepOptions = new ProductOptionsRepository(TestProdConext);
            TestContOptions = new ProductOptionsController(TestRepOptions);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            Database.SetInitializer<ProductContext>(null);

            dummyProduct = null;
            TestRep = null;
            TestCont = null;
        }

        #region Product Testing
        [TestMethod]
        public void Product_AddProduct()
        {
            TestCont.Create(dummyProduct);
            var searchedProduct = FindProduct(dummyProduct.Id);

            Assert.AreEqual(dummyProduct.Id, searchedProduct.Id);
        }

        [TestMethod]
        public void Product_GetAllProducts()
        {
            var searchedProduct = TestCont.GetAll();

            var content = searchedProduct as OkNegotiatedContentResult<List<Product>>;
            Assert.AreNotEqual(0, content.Content.Count);
        }

        [TestMethod]
        public void Product_FindProductByName()
        {
            var response = TestCont.SearchByName(dummyProduct.Name);

            var content = response as OkNegotiatedContentResult<List<Product>>;

            Assert.AreEqual(1, content.Content.Count);
        }

        [TestMethod]
        public void Product_UpdateProduct()
        {
            Product updatedProduct = new Product()
            {
                Id = new System.Guid("9756ef78-295d-44f7-a34c-614cb326d14c"),
                Name = "Xero-Accounting-Software",
                Price = 90000,
                DeliveryPrice = 95000,
                Description = "Pay-Bills"
            };

            TestCont.Update(dummyProduct.Id, updatedProduct);
            Product prd = FindProduct(updatedProduct.Id);
            dummyProduct.Id = new System.Guid("9756ef78-295d-44f7-a34c-614cb326d14c");
            Assert.AreEqual(updatedProduct.Id, prd.Id);
        }

        [TestMethod]
        public void Product_FindProductById()
        {
            var response = FindProduct(dummyProduct.Id);
            Assert.AreEqual(dummyProduct.Id, response.Id);
        }

        public Product FindProduct(Guid guid)
        {
            var response = TestCont.SearchById(guid);
            var content = response as OkNegotiatedContentResult<List<Product>>;

            if (content.Content.Count == 0)
            {
                return null;
            }
            return (Product)content.Content[0];
        }

        [TestMethod]
        public void Product_Z_DeleteProduct()
        {
            TestCont.Remove(dummyProduct.Id);
            Product prd = FindProduct(dummyProduct.Id);
            Assert.AreEqual(null, prd);
        }


        #endregion

        #region Product Options
        [TestMethod]
        public void Product_Options_AddProductOptions()
        {
            TestContOptions.Create(dummyProductOptions);
            var searchedProduct = FindProductOptions(dummyProductOptions.ProductId);

            Assert.AreEqual(dummyProductOptions.Id, searchedProduct.Id);
        }

        [TestMethod]
        public void Product_Options_UpdateProductOptions()
        {
            ProductOptions updatedProductOptions = new ProductOptions()
            {
                Id = new System.Guid("1979ef78-295d-44f7-a34c-614cb326d14c"),
                ProductId = new System.Guid("3756ef78-295d-44f7-a34c-614cb326d14c"),
                Name = "Xero-Accounting-Software",
                Description = "Prod Options Desc"
            };
            TestContOptions.Update(dummyProductOptions.Id, updatedProductOptions);
            var searchedProduct = FindProductOptions(dummyProductOptions.ProductId);

            Assert.AreEqual(dummyProductOptions.Id, searchedProduct.Id);
        }

        [TestMethod]
        public void Product_Options_FindOptions()
        {
           ProductOptions prd= FindProductOptions(dummyProductOptions.ProductId);
            Assert.AreEqual(dummyProductOptions.Id, prd.Id);
        }

        public ProductOptions FindProductOptions(Guid guid)
        {
            var response = TestContOptions.SearchById(guid);
            var content = response as OkNegotiatedContentResult<List<ProductOptions>>;

            if (content.Content.Count == 0)
            {
                return null;
            }
            return (ProductOptions)content.Content[0];
        }

        [TestMethod]
        public void Product_Options_DeleteProductOptions()
        {
            TestContOptions.Remove(dummyProductOptions.Id);

            var searchedProduct = FindProductOptions(dummyProductOptions.Id);

            Assert.AreEqual(null, searchedProduct);
        }
        #endregion


        static void SetupDemoProduct()
        {
            dummyProduct = new  Product() {
                Id = new System.Guid("3756ef78-295d-44f7-a34c-614cb326d14c"),
                Name = "Xero-Accounting-Software",
                Price = 90000,
                DeliveryPrice=95000,
                Description= "Pay-Bills"
            };
        }

        static void SetupDemoProductOptions()
        {
            dummyProductOptions = new ProductOptions()
            {
                Id = new System.Guid("0756ef78-295d-44f7-a34c-614cb326d14c"),
                ProductId = new System.Guid("3756ef78-295d-44f7-a34c-614cb326d14c"),
                Name = "Xero-Accounting-Software",
                Description = "Prod Options Desc"
            };
        }
    }
}