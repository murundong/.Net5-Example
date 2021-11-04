using Castle.Windsor;
using CoreBaseClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEntityFramework.DbContextProvider
{
    public class DbContextProvider<TdbContext> : IDbContextProvider<TdbContext>
       where TdbContext : DbContext
    {
        static string key = $"DbContext_Single_{typeof(TdbContext).Name}";
        private static readonly object obj = new object();
        private IWindsorContainer _windsorContainer;

        public DbContextProvider(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }
        public TdbContext GetDbContext()
        {
            lock (obj)
            {
                DbContext temp = CallContext.GetData(key) as DbContext;

                if (temp == null)
                {
                    temp = _windsorContainer.Resolve<TdbContext>();
                    //temp = Activator.CreateInstance<TdbContext>();
                    CallContext.SetData(key, temp);
                }
                return temp as TdbContext;
            }
        }

        public void Release()
        {
            lock (obj)
            {
                var dbContext = CallContext.GetData(key) as DbContext;
                if (dbContext != null)
                {
                    dbContext.SaveChanges();
                    dbContext.Dispose();
                    dbContext = null;
                    CallContext.SetData(key, null);
                }
            }
        }
    }
}
