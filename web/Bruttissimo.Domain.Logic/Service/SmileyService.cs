using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class SmileyService : ISmileyService
    {
        private readonly ISmileyRepository smileyRepository;

        public SmileyService(ISmileyRepository smileyRepository)
        {
            Ensure.That(() => smileyRepository).IsNotNull();

            this.smileyRepository = smileyRepository;
        }

        public IList<Smiley> GetSmileys()
        {
            IEnumerable<Smiley> smileys = smileyRepository.GetSmileys();
            return smileys.ToList();
        }
    }
}
