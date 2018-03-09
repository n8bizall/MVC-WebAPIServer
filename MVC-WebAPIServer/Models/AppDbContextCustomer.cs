using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_WebAPIServer.Models
{
    public class AppDbContextCustomer
    {
        public AppDbContextCustomer() : base()
        { }
        public DbSet<Customer> Customer { get; set; }
    }
}