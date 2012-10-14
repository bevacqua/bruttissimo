using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Utility;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly TextHelper textHelper;
        private readonly ICommentService commentService;

        public PostService(IPostRepository postRepository, ICommentService commentService, TextHelper textHelper)
        {
            Ensure.That(() => postRepository).IsNotNull();
            Ensure.That(() => commentService).IsNotNull();
            Ensure.That(() => textHelper).IsNotNull();

            this.postRepository = postRepository;
            this.commentService = commentService;
            this.textHelper = textHelper;
        }

        public Post Create(Link link, string message, User user)
        {
            Post post = postRepository.Insert(link, message, user);
            return post;
        }

        public Post GetById(long id, bool includeLink = false, bool includeComments = false)
        {
            Post post = postRepository.GetById(id, includeLink);
            if (includeComments)
            {
                post.Comments = commentService.GetComments(post);
            }
            return post;
        }

        public IEnumerable<Post> GetLatest(long? timestamp, int count)
        {
            DateTime? until = null;
            if (timestamp.HasValue)
            {
                until = DateTime.FromBinary(timestamp.Value);
            }
            IEnumerable<Post> posts = postRepository.GetLatest(until, count);
            return posts;
        }

        public string GetTitleSlug(Post post)
        {
            if (post.Link == null)
            {
                return textHelper.Slugify(post.UserMessage, 40);
            }
            else
            {
                return textHelper.Slugify(post.Link.Title);
            }
        }

        /// <summary>
        /// Parses links and smileys in user input, removing undersired links, highlighting
        /// the others, and turning smilies into their appropriate image tags.
        /// </summary>
        /// <param name="message">A message provided by a user.</param>
        /// <returns>The message formatted how we want to display it.</returns>
        public IHtmlString BeautifyUserMessage(string message)
        {
            if (message == null)
            {
                return null;
            }
            string encoded = HttpUtility.HtmlEncode(message);

            // TODO links.

            ISmileyService smileyService = IoC.Container.Resolve<ISmileyService>();
            IEnumerable<SmileyDto> replacements = smileyService.GetSmileyReplacements();
            foreach (SmileyDto replacement in replacements)
            {
                string smiley = replacement.Smiley.Value;

                foreach (string keyword in replacement.EncodedKeywords)
                {
                    encoded = encoded.Replace(keyword, smiley, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            IHtmlString html = new MvcHtmlString(encoded);
            return html;
        }
    }
}
