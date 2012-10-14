using System.Collections.Generic;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface ISmileyService
    {
        IList<Smiley> GetSmileys();
        IEnumerable<SmileyDto> GetSmileyReplacements();
    }
}
