using System;
using System.Web.Http;
using refactor_me.Xero___Repository;
using refactor_me.Models;
using System.Data.Entity;
using System.Net;

namespace refactor_me.Controllers
{
    [RoutePrefix("ProductOptions")]
    public class ProductOptionsController : ApiController
    {
        private IProductRepository<ProductOptions> _productRepository;

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

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
            try
            {
                var products = _productRepository.Get();
                return Ok(products);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(ProductOptions product)
        {
            if(product == null)
            {
                return Ok(BadRequest());
            }

            try
            {
                _productRepository.Insert(product);
                _productRepository.Save();
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);

        }

        /// <summary>
        /// Update the existing Product Options
        /// </summary>
        /// <param name="id">Existing Id of the Product</param>
        /// <param name="product">Updated Product Information</param>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(Guid id, ProductOptions product)
        {
            if (id == null || product == null)
                return Ok(BadRequest());
            try {
                EntityState state = _productRepository.Update(id, product);
                if (state != EntityState.Added)
                    _productRepository.Save();
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);

        }

        /// <summary>
        /// Remove the Product Options
        /// </summary>
        /// <param name="productOptionsGuId">Id of the Product</param>
        [Route("Remove")]
        [HttpPost]
        public IHttpActionResult Remove(Guid productOptionsGuId)
        {
            if(productOptionsGuId == null)
            {
                return Ok(BadRequest());
            }
            try
            {
                _productRepository.Delete(productOptionsGuId);
                _productRepository.Save();
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);
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
            if (name == null || name == "")
            {
                return BadRequest();
            }
            try
            {
                var product = _productRepository.Find(name);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);

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
            if(id == null)
            {
                return Ok(BadRequest());
            }
            try
            {
                var product = _productRepository.Find(id);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);
        }
    }
}