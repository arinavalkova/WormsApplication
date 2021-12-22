using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EntitiesLibrary.entities;
using EntitiesLibrary.entities.commands;
using WormsApplication.entities;

namespace WormsApplication.services.way
{
    public class WayGetter
    {
        public async Task<Command?> Get(WorldState worldState, Worm worm)
        {
            string jsonString = JsonSerializer.Serialize(worldState);
            HttpResponseMessage response = await new HttpClient().PostAsync(
                $"http://localhost:5000/worms/{worm.Name}/getAction",
                new StringContent(jsonString, Encoding.UTF8, "application/json"));
            return response.Content.ReadFromJsonAsync<Command>().Result;
        }
    }
}