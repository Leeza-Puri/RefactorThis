﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using refactor_me.Xero___Repository;
using refactor_me.Models;
using System.Data.Entity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Configuration;

namespace refactor_this.Controllers
{
#if !DEBUG //only add the auth if its in release mode
    [Authorize]
#endif
    [RoutePrefix("Products")]
    public class ProductsController : ApiController
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private IProductRepository<Product> _productRepository;


        public ProductsController(IProductRepository<Product> productRep)
        {
            _productRepository = productRep;
        }

        /// <summary>
        /// Get all the Products
        /// </summary>
        /// <returns>List of Products in Json format.</returns>
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var products = _productRepository.Get();
                return Ok(products);
            }
            catch(System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(BadRequest());
        }

        /// <summary>
        /// Create the new Product
        /// </summary>
        /// <param name="product">Product Object</param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            if (product == null)
                return (BadRequest());
            try
            {
                _productRepository.Insert(product);
                _productRepository.Save();
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return Ok(BadRequest());
        }

        /// <summary>
        /// Update the Product against the Product ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(Guid id, Product product)
        {
            if(id == null || product == null)
            {
                return Ok(BadRequest());
            }
            try
            {
                EntityState state = _productRepository.Update(id, product);
                if (state != EntityState.Added)
                    _productRepository.Save();
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(BadRequest());
        }

        /// <summary>
        /// Remove the existing Product
        /// </summary>
        /// <param name="productId">Id of the Product to delete</param>
        [Route("Remove")]
        [HttpPost]
        public IHttpActionResult Remove(Guid productId)
        {
            if (productId == null)
                return Ok(BadRequest());

            try
            {
                _productRepository.Delete(productId);
                _productRepository.Save();
                return Ok(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(HttpStatusCode.BadRequest);

        }


        /// <summary>
        /// Get the Product information
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
        /// Get the Product detail
        /// </summary>
        /// <param name="id">Id of the Product</param>
        /// <returns></returns>
        [Route("FindById")]
        [HttpGet]
        public IHttpActionResult SearchById(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
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

        private string CraftJwt()
        {
            try
            {
                string key = ConfigurationManager.AppSettings["jwt_signing_secret_key"]; //Secret key which will be used later during validation    
                var issuer = ConfigurationManager.AppSettings["JWT-ValidIssuer"];
                string validAudience = ConfigurationManager.AppSettings["JWT-ValidAudience"];

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("valid", "1"),
                new Claim("userid", "1"),
                new Claim("name", "test")
            };

                var token = new JwtSecurityToken(issuer,
                    validAudience,
                    permClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return "Error:";
        }

        /// <summary>
        /// Temporary end point for testing purpose to verify the token bearer auth
        /// </summary>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpGet]
        [AllowAnonymous]
        public string GetToken()
        {
            return "{CraftJwt()}";
        }
    }
}

