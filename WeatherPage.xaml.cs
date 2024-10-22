using Newtonsoft.Json;

namespace LifeManagementApp;

public partial class Weather : ContentPage
{
	public Weather()
	{
		InitializeComponent();
	}

	public async void ForecastButton(object sender, EventArgs e)
	{
		double latitude = Double.Parse(Latitude.Text);
		double longitude = Double.Parse(Longitude.Text);
        dynamic resp = await ForecastRequest.CallForecast(latitude, longitude);

        High.Text = $"Todays High: {resp.daily.temperature_2m_max[0]}{resp.daily_units.temperature_2m_max}";
		Low.Text = $"Todays Low: {resp.daily.temperature_2m_min[0]}{resp.daily_units.temperature_2m_min}";
		Rain.Text = $"Rain Forecast: {resp.daily.rain_sum[0]}{resp.daily_units.rain_sum}";
		Wind.Text = $"Max Wind Speed: {resp.daily.wind_speed_10m_max[0]}{resp.daily_units.wind_speed_10m_max}";
		if (resp.daily.wind_speed_10m_max[0] >= 4.0)
		{
			Wind_Alert.Text = "!!High Wind Speed!!";
			await DisplayAlert("Wind Speed Alert", "The wind speed is over 4.0m/s. Today is a great day for kite Surfing!!", "Ok");
		}
		else
		{
			Wind_Alert.Text = "Low Wind Speed";
		}

    }
}