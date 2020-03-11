using XboxGameClipLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace XboxGameClipLibrary.API
{
    public class Api
    {
        public class ApiException : Exception
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }
        }

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

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }

        public static async Task<string> GetXuidFromStringCallAsync(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://xboxapi.com/v2/accountXuid"))
            {
                request.Headers.Add("X-Auth", "");

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonObject = JObject.Parse(jsonString);
                        return jsonObject.GetValue("xuid").ToString();
                    }

                    var content = jsonString;

                    throw new ApiException
                    {
                        StatusCode = (int) response.StatusCode,
                        Content = content
                    };
                }
            }
        }

        public static async Task<List<GameClip>> GetGameClipsFromStreamCallAsync(CancellationToken cancellationToken, string xuid)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://xboxapi.com/v2/" + xuid + "/game-clips"))
            {
                request.Headers.Add("X-Auth", "");

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return DeserializeJsonFromStream<List<GameClip>>(stream);
                    }

                    var content = await StreamToStringAsync(stream);

                    throw new ApiException
                    {
                        StatusCode = (int) response.StatusCode,
                        Content = content
                    };
                }
            }
        }
    }
}