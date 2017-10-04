using System;

namespace vc.data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        VCEntities Init();
    }
}