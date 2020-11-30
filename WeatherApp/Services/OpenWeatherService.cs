using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    public class OpenWeatherService : ITemperatureService
    {
        OpenWeatherProcessor owp;

        public OpenWeatherService(string apiKey)
        {
            owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = apiKey;
        }
        
        public async Task<TemperatureModel> GetTempAsync()
        {
            var temp = await owp.GetCurrentWeatherAsync();
            TemperatureModel tm = new TemperatureModel();

            if (temp.Cod == 401)
                MessageBox.Show("Clé invalide !");
            else if (temp.Cod == 404)
                MessageBox.Show("Ville introuvable !");
            else
            {
                tm = new TemperatureModel
                {
                    DateTime = DateTime.UnixEpoch.AddSeconds(temp.DateTime),
                    Temperature = temp.Main.Temperature
                };
            }

            return tm;
        }

        public void SetLocation(string location)
        {
            owp.City = location;
        }

        public void SetApiKey(string apikey)
        {
            owp.ApiKey = apikey;
        }
    }
}
