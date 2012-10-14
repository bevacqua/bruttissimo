using System;
using System.Collections.Generic;
using Bruttissimo.Domain.DTO.Link;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface ILinkService
    {
        LinkResult ParseUserInput(string text);
        IList<Uri> GetReferenceUris(string text);
        string HotLinkHtml(string html, Func<Uri, bool> filter);
        bool AreEqual(Uri uri, Link link);
    }
}
