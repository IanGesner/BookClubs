using BookClubs.Data.Configuration;
using System;

namespace BookClubs.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BcContext Init();
    }
}
