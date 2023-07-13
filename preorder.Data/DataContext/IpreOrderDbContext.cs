using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using preorder.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripleSix.Core.DataContext;

namespace preorder.Data.DataContext 
{
    internal interface IpreOrderDbContext : IDbDataContext
    {
        DbSet<sr_header> sr_header { get; set; }
    }
}
