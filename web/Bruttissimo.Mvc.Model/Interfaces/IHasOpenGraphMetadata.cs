using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Interfaces
{
    public interface IHasOpenGraphMetadata
    {
        OpenGraphModel OpenGraph { get; }
    }
}
