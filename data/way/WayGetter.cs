using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WormsApplication.entities;

namespace WormsApplication.data.way
{
    public class WayGetter
    {
        public async Task Get(WorldState worldState, Worm worm)
        {
            string jsonString = JsonSerializer.Serialize(worldState);
            HttpResponseMessage response = await new HttpClient().PostAsync("http://localhost:5000/worms/",
                new StringContent(jsonString, Encoding.UTF8, "application/json"));
            Console.WriteLine(response);
            //get request
            //convert json to Way
        }
    }
}