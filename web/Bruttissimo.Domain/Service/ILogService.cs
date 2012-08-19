using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ILogService
    {
        IList<Log> GetLast(int count);
    }
}
