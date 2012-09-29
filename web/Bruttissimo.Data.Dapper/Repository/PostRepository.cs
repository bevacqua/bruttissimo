using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Bruttissimo.Data.Dapper
{
    public class PostRepository : EntityRepository<Post>, IPostRepository
    {
        private readonly IDbConnection connection;

        public PostRepository(IDbConnection connection)
            : base(connection)
        {
            Ensure.That(connection, "connection").IsNotNull();

            this.connection = connection;
        }

        public Post GetById(long postId, bool includeLink)
        {
            if (includeLink)
            {
                const string sql = @"
					SELECT [Post].*, [Link].*
					FROM [Post]
					LEFT JOIN [Link] ON [Post].[LinkId] = [Link].[Id]
					WHERE [Post].[Id] = @postId
				";
                Func<Post, Link, Post> map = (post, link) =>
                {
                    post.Link = link;
                    return post;
                };
                IEnumerable<Post> posts = connection.Query(sql, map, new { postId });
                return posts.FirstOrDefault();
            }
            else
            {
                return base.GetById(postId);
            }
        }

        public IEnumerable<Post> GetLatest(DateTime? until, int count)
        {
            DateTime created = until ?? DateTime.UtcNow;
            const string sql = @"
				SET ROWCOUNT @count

				SELECT [Post].*, [Link].*, [User].*
				FROM [Post]
				LEFT JOIN [Link] ON [Post].[LinkId] = [Link].[Id]
				LEFT JOIN [User] ON [Post].[UserId] = [User].[Id]
				WHERE [Post].[Created] < @created
				ORDER BY [Post].[Created] DESC

				SET ROWCOUNT 0
			";
            Func<Post, Link, User, Post> map = (post, link, user) =>
            {
                post.Link = link;
                post.User = user;
                return post;
            };
            IEnumerable<Post> posts = connection.Query(sql, map, new { created, count });
            return posts;
        }

		public override Post Insert(Post entity)
        {
            Ensure.That(entity, "entity").IsNotNull();

			DateTime now = DateTime.UtcNow;
			entity.Created = now;
			entity.Updated = now;
			return base.Insert(entity);
		}

		public Post Insert(Link link, string message, User user)
        {
            Ensure.That(link, "link").IsNotNull();
            Ensure.That(user, "user").IsNotNull();

            DateTime now = DateTime.UtcNow;
            Post post = new Post
            {
                Created = now,
                Updated = now,
                LinkId = link.Id,
                UserId = user.Id,
                UserMessage = message
            };
            link.PostId = connection.Insert(post);
            return post;
        }

        public IEnumerable<Post> GetPostsPendingFacebookExport()
        {
            const string sql = @"
                SELECT [Post].*, [Link].*
                FROM [Post]
                LEFT JOIN [Link] ON [Link].[Id] = [Post].[LinkId]
                WHERE [Post].[FacebookPostId] = NULL
            ";
            Func<Post, Link, Post> map = (post, link) =>
            {
                post.Link = link;
                return post;
            };
            IEnumerable<Post> posts = connection.Query(sql, map);
            return posts;
        }

        public IEnumerable<Post> GetPostsPendingTwitterExport()
        {
            const string sql = @"
                SELECT [Post].*, [Link].*
                FROM [Post]
                LEFT JOIN [Link] ON [Link].[Id] = [Post].[LinkId]
                WHERE [Post].[TwitterPostId] = NULL
                AND [Post].[TwitterUserId] != NULL
            ";
            Func<Post, Link, Post> map = (post, link) =>
            {
                post.Link = link;
                return post;
            };
            IEnumerable<Post> posts = connection.Query(sql, map);
            return posts;
        }
    }
}
