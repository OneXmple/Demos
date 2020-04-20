using System;

namespace Httpgrpc.Common.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
         Guid UserId { get; set; }
    }
}