using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
 
namespace SpotifyClone
{
    public class WebRequest
    {
        // The second parameter is a function that returns a Dictionary of string keys to object values.
        // If an API returned an array as its top level collection the parameter type would be "Action>"
        public static async Task GetArtist(string ArtistSearch, Action<JObject> Callback)
        {
            // Create a temporary HttpClient connection.
            using (var Client = new HttpClient())
            {
                try
                {
                    
                    Client.BaseAddress = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={ArtistSearch}&api_key=d2aa77064a676630625737264efb732c&format=json");
                    HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                    Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                    string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.
                    // System.Console.WriteLine("=================apicaller StringResponse", StringResponse.GetType());
                    // Then parse the result into JSON and convert to a dictionary that we can use.
                    // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
                    JObject JsonResponse = JsonConvert.DeserializeObject<JObject>(StringResponse);
                    // System.Console.WriteLine("==================apicaller JsonResponse", JsonResponse.GetType());

                    // Finally, execute our callback, passing it the response we got.
                    Callback(JsonResponse);

                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }

        public static async Task GetAlbum(string AlbumSearch, Action<JObject> Callback)
        {
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri($"http://ws.audioscrobbler.com/2.0/?method=album.search&album={AlbumSearch}&api_key=d2aa77064a676630625737264efb732c&format=json");
                    HttpResponseMessage Response = await Client.GetAsync(""); 
                    Response.EnsureSuccessStatusCode(); 
                    string StringResponse = await Response.Content.ReadAsStringAsync(); 
                    // System.Console.WriteLine("=================apicaller StringResponse", StringResponse.GetType());
                    
                    JObject JsonResponse = JsonConvert.DeserializeObject<JObject>(StringResponse);
                    // System.Console.WriteLine("==================apicaller JsonResponse", JsonResponse.GetType());

                    Callback(JsonResponse);

                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }
    }
}
