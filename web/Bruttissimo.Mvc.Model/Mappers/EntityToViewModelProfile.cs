using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Quartz;

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
            CreateJobMaps();
            CreateMap<Log, LogModel>();
        }

        internal void CreateJobMaps()
        {
            CreateMap<Type, JobDto>().ForMember(
                m => m.Name,
                x => x.MapFrom(t => t.Name.Replace("Job", string.Empty).SplitOnCamelCase())
            ).ForMember(
                m => m.Guid,
                x => x.MapFrom(t => t.GUID.Stringify())
            );

            CreateMap<JobDto, JobModel>();

            CreateMap<IJobExecutionContext, ScheduledJobDto>().ForMember(
                m => m.Name,
                x => x.MapFrom(c => c.JobDetail.JobType.Name.Replace("Job", string.Empty).SplitOnCamelCase())
            ).ForMember(
                m => m.Guid,
                x => x.MapFrom(c => c.JobDetail.JobType.GUID.Stringify())
            ).ForMember(
                m => m.RunTime,
                x => x.MapFrom(c => c.JobRunTime)
            ).ForMember(
                m => m.FireTime,
                x => x.MapFrom(c => c.FireTimeUtc ?? c.ScheduledFireTimeUtc ?? c.PreviousFireTimeUtc)
            );

            CreateMap<ScheduledJobDto, ScheduledJobModel>().ForMember(
                m => m.RunTime,
                x => x.MapFrom(c => c.RunTime.ToDurationString())
            ).ForMember(
                m => m.FireTime,
                x => x.MapFrom(c => c.FireTime) // TODO.
            );
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