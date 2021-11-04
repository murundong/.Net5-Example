using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEntityFramework.DbContextProvider
{
    public interface IDbContextProvider<out TDbContext>
       where TDbContext : DbContext
    {
        TDbContext GetDbContext();

        void Release();
    }
}
