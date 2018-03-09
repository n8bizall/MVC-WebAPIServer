﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebAPIServer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [StringLength(50)] [Required]
        public string Name { get; set; }
        [Required]
        public decimal CreditLimit{ get; set; }
        [Required]
        public bool Active { get; set; }
    }
}