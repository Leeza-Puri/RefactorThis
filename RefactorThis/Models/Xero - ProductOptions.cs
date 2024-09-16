using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    /// <summary>
    /// Model class for Production options for the product.
    /// </summary>
    [Table("ProductOption")]
    public class ProductOptions
    {
        [Key]
        public Guid Id { get; set; }

        //[ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}