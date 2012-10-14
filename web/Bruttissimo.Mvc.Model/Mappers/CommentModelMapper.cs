using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers
{
    public class CommentModelMapper : IMapperConfigurator
    {
        private readonly IPostService postService;

        public CommentModelMapper(IPostService postService)
        {
            Ensure.That(() => postService).IsNotNull();

            this.postService = postService;
        }

        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Comment, CommentModel>()
                .ForMember(
                    dest => dest.Message,
                    opt => opt.MapFrom(src => postService.BeautifyUserMessage(src.Message, null)));
        }
    }
}