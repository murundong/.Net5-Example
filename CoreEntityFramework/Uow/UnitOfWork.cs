using CoreEntityFramework.DbContextProvider;
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
        private IDbContextProvider<TDbContext> _dbContextProvider;
        public UnitOfWork(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        public virtual TDbContext _dbContext => _dbContextProvider.GetDbContext();
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
