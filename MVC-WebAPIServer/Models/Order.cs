using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace MVC_WebAPIServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [StringLength(80)] [Required]
        public string  Description { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public bool Fulfilled { get; set; }

        public virtual Customer Customer { get; set; }
    }
}