using System;

namespace Httpgrpc.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);     
    }
}