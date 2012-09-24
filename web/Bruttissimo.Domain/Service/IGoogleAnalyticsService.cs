namespace Bruttissimo.Domain
{
    public interface IGoogleAnalyticsService
    {
        string BuildPixelUrl(string analyticsID, string domain, string referer, string title, string data);
    }
}
