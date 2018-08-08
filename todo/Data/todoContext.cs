using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.Model;

namespace todo.Models
{
    public class todoContext : DbContext
    {
        public todoContext (DbContextOptions<todoContext> options)
            : base(options)
        {
        }

        public DbSet<todo.Model.Data> Data { get; set; }
    }
}
