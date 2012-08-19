using System;
using AutoMapper;
using Bruttissimo.Common;
using Quartz;

namespace Bruttissimo.Domain.Entity
{
    public class EntityToDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateJobMaps();
            CreateFacebookMaps();
        }

        internal void CreateFacebookMaps()
        {
            CreateMap<FacebookPost, Link>().ForMember(
                m => m.ReferenceUri,
                x => x.MapFrom(p => p.Link)
            ).ForMember(
                m => m.Title,
                x => x.MapFrom(p => p.Name)
            ).ForMember(
                m => m.Description,
                x => x.MapFrom(p => p.Description)
            ).ForMember(
                m => m.Picture,
                x => x.MapFrom(p => p.Picture)
            ).ForMember(
                m => m.Type,
                x => x.UseValue(LinkType.None)
            ).ForMember(
                m => m.Created,
                x => x.MapFrom(p => DateTime.UtcNow)
            ).Ignoring(
                m => m.Id,
                m => m.PostId,
                m => m.Type
            );

            CreateMap<FacebookPost, Post>().ForMember(
                m => m.FacebookPostId,
                x => x.MapFrom(p => p.Id)
            ).ForMember(
                m => m.FacebookFeedId,
                x => x.MapFrom(p => p.To.Data[0].Id)
            ).ForMember(
                m => m.FacebookUserId,
                x => x.MapFrom(p => p.From.Id)
            ).ForMember(
                m => m.UserMessage,
                x => x.MapFrom(p => p.Message)
            ).ForMember(
                m => m.Created,
                x => x.MapFrom(p => DateTime.UtcNow)
            ).Ignoring(
                m => m.Id,
                m => m.UserId,
                m => m.User,
                m => m.LinkId,
                m => m.Link,
                m => m.Updated
            );
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
        }
    }
}