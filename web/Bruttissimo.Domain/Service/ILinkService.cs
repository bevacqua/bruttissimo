using System;
using System.Collections.Generic;
using Bruttissimo.Domain.DTO.Link;

namespace Bruttissimo.Domain.Service
{
    public interface ILinkService
    {
        LinkResult ParseUserInput(string text);
        IList<Uri> GetReferenceUris(string text);
    }
}
