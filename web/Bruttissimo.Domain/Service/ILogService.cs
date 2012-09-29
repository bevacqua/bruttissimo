using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface ILogService
    {
        IList<Log> GetLast(int count);
    }
}
