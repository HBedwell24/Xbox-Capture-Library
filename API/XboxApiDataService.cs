using XboxGameClipLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using XboxGameClipLibrary.Models.Screenshots;
using XboxGameClipLibrary.Views.Pages;

namespace XboxGameClipLibrary.API
{
    public class XboxApiDataService
    {
        public class ApiException : Exception { }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default;

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        public static async Task<JObject> GetProfileFromStringCallAsync(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://xapi.us/v2/profile"))
            {
                request.Headers.Add("X-Auth", Properties.Settings.Default.xboxApiKey);

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var content = "";

                    try
                    {
                        var stream = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            return JObject.Parse(stream);
                        }
                        else
                        {
                            content = stream;
                            throw new ApiException();
                        }
                    }
                    catch (ApiException)
                    {
                        Navigation.Navigation.Navigate(new ExceptionPage((int) response.StatusCode, content));
                    }

                    return null;
                }
            }
        }

        public static async Task<List<Screenshot>> GetScreenshotsFromStreamCallAsync(CancellationToken cancellationToken, string xuid)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://xapi.us/v2/" + xuid + "/screenshots"))
            {
                request.Headers.Add("X-Auth", Properties.Settings.Default.xboxApiKey);

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return DeserializeJsonFromStream<List<Screenshot>>(stream);
                    }

                    return null;
                }
            }
        }

        public static async Task<List<GameClip>> GetGameClipsFromStreamCallAsync(CancellationToken cancellationToken, string xuid)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://xapi.us/v2/" + xuid + "/game-clips"))
            {
                request.Headers.Add("X-Auth", Properties.Settings.Default.xboxApiKey);

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return DeserializeJsonFromStream<List<GameClip>>(stream);
                    }

                    return null;
                }
            }
        }
    }
}