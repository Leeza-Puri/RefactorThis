using System;
using System.Web.Http;
using refactor_me.Xero___Repository;
using refactor_me.Models;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace refactor_me.Controllers
{
    [RoutePrefix("ProductOptions")]
    public class ProductOptionsController : ApiController
    {
        private IProductRepository<ProductOptions> _productRepository;

        public ProductOptionsController(IProductRepository<ProductOptions> productRep)
        {
            _productRepository = productRep;
        }


        /// <summary>
        /// Get all the Production Options
        /// </summary>
        /// <returns>List of Products Options</returns>
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var products = _productRepository.Get();
            return Ok(products);
        }

        [Route("Create")]
        [HttpPost]
        public void Create(ProductOptions product)
        {
            _productRepository.Insert(product);
            _productRepository.Save();
        }

        /// <summary>
        /// Update the existing Product Options
        /// </summary>
        /// <param name="id">Existing Id of the Product</param>
        /// <param name="product">Updated Product Information</param>
        [Route("Update")]
        [HttpPost]
        public void Update(Guid id, ProductOptions product)
        {
            EntityState state = _productRepository.Update(id, product);
            if (state != EntityState.Added)
                _productRepository.Save();
        }

        /// <summary>
        /// Remove the Product Options
        /// </summary>
        /// <param name="productOptionsGuId">Id of the Product</param>
        [Route("Remove")]
        [HttpPost]
        public void Remove(Guid productOptionsGuId)
        {
            _productRepository.Delete(productOptionsGuId);
            _productRepository.Save();
        }

        /// <summary>
        /// Get the Product by Name.
        /// </summary>
        /// <param name="name">Name of the Product</param>
        /// <returns></returns>
        [Route("FindByName")]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var product = _productRepository.Find(name);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Get the Product Options by ID
        /// </summary>
        /// <param name="id">Id of the Production Options</param>
        /// <returns></returns>
        [Route("FindByProductId")]
        [HttpGet]
        public IHttpActionResult SearchById(Guid id)
        {
            var product = _productRepository.Find(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}