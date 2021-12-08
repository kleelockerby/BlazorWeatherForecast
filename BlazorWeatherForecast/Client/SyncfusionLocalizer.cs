using Syncfusion.Blazor;

namespace BlazorWeatherForecast.Client
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {
        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return BlazorWeatherForecast.Client.Resources.SfResources.ResourceManager;
            }
        }
    }
}
