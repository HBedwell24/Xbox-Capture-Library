using DashboardApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DashboardApplication.API
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
                return default(T);

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

        private static async Task<List<GameClip>> DeserializeOptimizedFromStreamCallAsync(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://xboxapi.com/v2/2535444128432735/game-clips"),
                    Method = HttpMethod.Get
                };

                request.Headers.Add("api-key", "API KEY GOES HERE");

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
