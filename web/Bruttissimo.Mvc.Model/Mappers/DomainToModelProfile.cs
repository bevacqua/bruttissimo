using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class DomainToModelProfile : Profile
    {
        private readonly IPostService postService;
		private readonly IUserService userService;
		private readonly UrlHelper urlHelper;

        public DomainToModelProfile(IPostService postService, IUserService userService, UrlHelper urlHelper)
        {
            if (postService == null)
            {
                throw new ArgumentNullException("postService");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }
            this.postService = postService;
            this.userService = userService;
            this.urlHelper = urlHelper;
        }

        protected override void Configure()
        {
            CreateCommentMaps();
            CreatePostMaps();
            CreateJobMaps();
            CreateMap<Log, LogModel>();
        }

        internal void CreateCommentMaps()
        {
            CreateMap<Comment, CommentModel>();
            CreateMap<Post, CommentListModel>().ConvertUsing<CommentListFromEntitiesConverter>();
        }

        internal void CreatePostMaps()
        {
            CreateMap<Post, PostModel>().ConvertUsing<PostFromEntityConverter>();

            PostModelMemberMappings(CreateMap<Post, LinkPostModel>());
            PostModelMemberMappings(CreateMap<Post, ImagePostModel>());

            CreateMap<Post, OpenGraphModel>().ForMember(
                m => m.Title,
                x => x.MapFrom(p => p.Link.Title)
            ).ForMember(
                m => m.Description,
                x => x.MapFrom(p => p.Link.Description)
            ).ForMember(
                m => m.Image,
                x => x.MapFrom(p => p.Link.Picture)
            ).ForMember(
                m => m.Url,
                x => x.MapFrom(p => urlHelper.RouteUrl("PostShortcut", new { id = p.Id }, "http"))
            );

            CreateMap<Link, LinkModel>();

            CreateMap<IEnumerable<Post>, PostListModel>().ConvertUsing<PostListFromEntitiesConverter>();
        }

        internal IMappingExpression<Post, T> PostModelMemberMappings<T>(IMappingExpression<Post, T> expression) where T : PostModel
        {
            return expression.ForMember(
                m => m.PostSlug,
                x => x.MapFrom(p => postService.GetTitleSlug(p))
            ).Ignoring(m => m.Comments);
        }

        internal void CreateJobMaps()
        {
            CreateMap<JobDto, JobModel>();

            CreateMap<ScheduledJobDto, ScheduledJobModel>().ForMember(
                m => m.StartTime,
                x => x.MapFrom(j => userService.ToCurrentUserTimeZone(HttpContext.Current.Wrap(), j.StartTime))
            );
        }

    }
}
