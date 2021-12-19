using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WormsApplication.data.way
{
    public class WayGetter
    {
        public async Task Get(World world, int id)
        {
            string jsonString = JsonSerializer.Serialize(new WorldConverter().Convert(world));
            File.WriteAllText("fileName.txt", jsonString);
            // HttpResponseMessage response = await new HttpClient().PostAsync("http://localhost:5000/worms/",
            //     new StringContent(JsonSerializer.Serialize(new WorldConverter().Convert(world)), Encoding.UTF8,
            //         "application/json"));

            //get request
            //convert json to Way
        }
    }
}