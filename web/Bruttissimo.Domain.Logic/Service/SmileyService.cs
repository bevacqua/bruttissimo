using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Static;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Logic.Service.Resources;
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

        public IEnumerable<SmileyDto> GetSmileyReplacements()
        {
            IList<Smiley> smileys = GetSmileys();

            var replacements = smileys.Select(x =>
            {
                string aliases = x.Aliases ?? string.Empty;
                IEnumerable<string> keywords = new[] { x.Emoticon }.Concat(aliases.SplitNonEmpty(' '));

                Lazy<string> smiley = new Lazy<string>(() =>
                {
                    string name = Smileys.ResourceManager.GetString(x.ResourceKey);
                    TagBuilder tag = new TagBuilder("img");
                    tag.Attributes.Add("title", x.Emoticon);
                    tag.Attributes.Add("alt", name);
                    tag.Attributes.Add("src", Config.Site.Pixel);
                    tag.AddCssClass(x.CssClass);
                    string source = tag.ToString(TagRenderMode.SelfClosing);
                    return source;
                });
                return new SmileyDto
                {
                    Keywords = keywords,
                    EncodedKeywords = keywords.Select(HttpUtility.HtmlEncode),
                    Smiley = smiley
                };
            });
            return replacements;
        }
    }
}
