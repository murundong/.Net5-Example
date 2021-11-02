using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEntityFramework.Uow
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
      where TDbContext : DbContext
    {
        private TDbContext _dbContext;
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }
    }
}
