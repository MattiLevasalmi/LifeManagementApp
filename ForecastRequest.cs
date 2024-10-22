using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeManagementApp
{
    class ForecastRequest
    {
        public static async Task<dynamic> CallForecast(double latitude, double longitude)
        {
            String Url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,rain_sum,wind_speed_10m_max&wind_speed_unit=ms&timezone=auto&forecast_days=1";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(Url);
            dynamic resp = JsonConvert.DeserializeObject<dynamic>(response.ToString());
            return resp;
        }
    }
}
