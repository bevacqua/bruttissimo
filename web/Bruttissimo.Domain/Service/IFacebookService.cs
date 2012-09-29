namespace Bruttissimo.Domain.Service
{
    public interface IFacebookService
    {
        void Import(string feed);
        void Export();
    }
}
