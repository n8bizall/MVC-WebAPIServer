using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebAPIServer.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int LineNbr { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal LineTotal { get; set; }

        public virtual Order Order { get; set; }

        public decimal CalcLineTotal()
        {
          LineTotal = (Price * Quantity);
            return CalcLineTotal();
        }
    }
    
}