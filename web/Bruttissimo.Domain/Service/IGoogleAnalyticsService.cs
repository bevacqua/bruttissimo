namespace Bruttissimo.Domain.Service
{
    public interface IGoogleAnalyticsService
    {
        string BuildPixelUrl(string analyticsID, string host, string referer, string absolute, string title, string data);
    }
}
