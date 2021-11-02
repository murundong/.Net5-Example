using Microsoft.EntityFrameworkCore;
using System;

namespace CoreEntityFramework
{
    public class App_DbContext:DbContext
    {
        public App_DbContext(DbContextOptions<App_DbContext> options):base(options)
        {


        }
    

    }
}
