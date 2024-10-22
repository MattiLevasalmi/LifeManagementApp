using Newtonsoft.Json;

namespace LifeManagementApp;

public partial class Weather : ContentPage
{
	HttpRequests RequestClient;
	public Weather()
	{
		InitializeComponent();
		RequestClient = new HttpRequests();
	}

	public async void ForecastButton(object sender, EventArgs e)
	{
		string cityName = CityName.Text;
		dynamic city = null;
		try
		{
			city = await RequestClient.FindCity(cityName);
			if (city.results == null)
			{
				await DisplayAlert("City Not Found", "The City you entered could not be found, please check the entered text.", "Okay");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "Okay?");
		}
		
		if (city.results != null)
		{
            double latitude = city.results[0].latitude;
            double longitude = city.results[0].longitude;
			dynamic weather = null;
			try
			{
				weather = await RequestClient.CallForecast(latitude, longitude);
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "Okay?");
			}

			if (weather != null)
			{
                Country.Text = city.results[0].country_code;
                High.Text = $"Todays High: {weather.daily.temperature_2m_max[0]}{weather.daily_units.temperature_2m_max}";
                Low.Text = $"Todays Low: {weather.daily.temperature_2m_min[0]}{weather.daily_units.temperature_2m_min}";
                Rain.Text = $"Rain Forecast: {weather.daily.rain_sum[0]}{weather.daily_units.rain_sum}";
                Wind.Text = $"Max Wind Speed: {weather.daily.wind_speed_10m_max[0]}{weather.daily_units.wind_speed_10m_max}";
                if (weather.daily.wind_speed_10m_max[0] >= 4.0)
                {
                    Wind_Alert.Text = "!!High Wind Speed!!";
                    await DisplayAlert("Wind Speed Alert", "The wind speed is over 4.0m/s. Today is a great day for kite surfing!!", "Okay");
                }
                else
                {
                    Wind_Alert.Text = "Low Wind Speed";
                }
            }
			else
			{
				await DisplayAlert("Whoops", "Sorry, it appears we couldn't load any weather data for this location.", "Okay?");
			}
        }
    }
}