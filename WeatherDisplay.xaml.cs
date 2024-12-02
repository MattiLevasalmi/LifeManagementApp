using Microsoft.Maui.Graphics.Text;
using Newtonsoft.Json;

namespace LifeManagementApp;

public partial class WeatherDisplay : ContentPage
{
    HttpRequests RequestClient;
    dynamic weather;
    int startTime = 0;
    public WeatherDisplay()
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
            weather = null;
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
                setTemp();
                High.Text = $"{weather.daily.temperature_2m_max[0]}{weather.daily_units.temperature_2m_max}";
                Low.Text = $"{weather.daily.temperature_2m_min[0]}{weather.daily_units.temperature_2m_min}";
                Rain.Text = $"{weather.daily.rain_sum[0]}{weather.daily_units.rain_sum}";
                Wind.Text = $"{weather.daily.wind_speed_10m_max[0]}{weather.daily_units.wind_speed_10m_max}";
                string fullRise = weather.daily.sunrise[0];
                string[] rise = fullRise.Split("T");
                Sunrise.Text = $"{rise[1]}";
                string fullSet = weather.daily.sunset[0];
                string[] set = fullSet.Split("T");
                Sunset.Text = $"{set[1]}";

                if (weather.daily.wind_speed_10m_max[0] >= 4.0)
                {
                    await DisplayAlert("Wind Speed Alert", "The wind speed is over 4.0m/s. Today is a great day for kite surfing!!", "Okay");
                }
            }
            else
            {
                await DisplayAlert("Whoops", "Sorry, it appears we couldn't load any weather data for this location.", "Okay?");
            }
        }
    }

    public void setAM(object sender, EventArgs e)
    {
        AM.TextColor = Color.Parse("MediumPurple");
        PM.TextColor = Color.Parse("White");
        startTime = 0;
        setTime();
        if (weather != null)
        {
            setTemp();
        }
    }

    public void setPM(object sender, EventArgs e)
    {
        AM.TextColor = Color.Parse("White");
        PM.TextColor = Color.Parse("MediumPurple");
        startTime = 12;
        setTime();
        if (weather != null)
        {
            setTemp();
        }
    }

    public void setTime()
    {
        List<Label> Times = new List<Label>([Time0, Time1, Time2, Time3, Time4, Time5, Time6, Time7, Time8, Time9, Time10, Time11]);

        for (int i = 0; i < Times.Count; i++)
        {
            Times[i].Text = $"{i + startTime}:00";
        }
    }

    public void setTemp()
    {
        List<Label> Temps = new List<Label>([Temp0, Temp1, Temp2, Temp3, Temp4, Temp5, Temp6, Temp7, Temp8, Temp9, Temp10, Temp11]);

        for (int i = 0; i < Temps.Count; i++)
        {
            Temps[i].Text = $"{weather.hourly.temperature_2m[i+startTime]}°C";
        }
    }
}