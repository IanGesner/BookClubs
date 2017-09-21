using BookClubs.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        BcContext dbContext;

        public BcContext Init()
        {
            return dbContext ?? (dbContext = new BcContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}