using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class EntityToViewModelProfile : Profile
    {
        private readonly IPostService postService;
        private readonly UrlHelper urlHelper;

        public EntityToViewModelProfile(IPostService postService, UrlHelper urlHelper)
        {
            if (postService == null)
            {
                throw new ArgumentNullException("postService");
            }
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }
            this.postService = postService;
            this.urlHelper = urlHelper;
        }

        protected override void Configure()
        {
            CreatePostMaps();

            CreateMap<Log, LogModel>();

            // jobs
            CreateMap<Type, JobDto>().ForMember(
                m => m.Name,
                x => x.MapFrom(t => t.Name.Replace("Job", string.Empty).SplitOnCamelCase())
            ).ForMember(
                m => m.Guid,
                x => x.MapFrom(t => t.GUID.Stringify())
            );

            CreateMap<JobDto, JobModel>();
            CreateMap<ScheduledJobDto, ScheduledJobModel>();
        }

        internal void CreatePostMaps()
        {
            CreateMap<Post, PostModel>().ConvertUsing<PostModelFromPostEntityConverter>();

            CreateMap<Post, LinkPostModel>().ForMember(
                m => m.PostSlug,
                x => x.MapFrom(p => postService.GetTitleSlug(p))
            );
            CreateMap<Post, ImagePostModel>().ForMember(
                m => m.PostSlug,
                x => x.MapFrom(p => postService.GetTitleSlug(p))
            );

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

            CreateMap<IEnumerable<Post>, PostListModel>().ConvertUsing<PostListModelFromPostEntityEnumerableConverter>();
        }
    }
}