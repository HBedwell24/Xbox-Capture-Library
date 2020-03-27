using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XboxGameClipLibrary.Models;
using XboxGameClipLibrary.Models.Screenshots;

namespace XboxGameClipLibrary.API
{
    public class XboxApiImpl
    {
        public static async Task<List<Screenshot>> GetScreenshots(CancellationToken token)
        {
            var xuid = await GetXuid(token);
            var screenshots = await XboxApiDataService.GetScreenshotsFromStreamCallAsync(token, xuid);

            // Debug Screenshot response
            string jsonString = JsonConvert.SerializeObject(screenshots, Formatting.Indented);
            Console.WriteLine(jsonString);

            return screenshots;
        }

        public static async Task<List<GameClip>> GetGameClips(CancellationToken token)
        {
            var xuid = await GetXuid(token);
            var gameClips = await XboxApiDataService.GetGameClipsFromStreamCallAsync(token, xuid);

            // Debug GameClip response
            string jsonString = JsonConvert.SerializeObject(gameClips, Formatting.Indented);
            Console.WriteLine(jsonString);

            return gameClips;
        }

        public static async Task<string> GetXuid(CancellationToken token)
        {
            var profile = await XboxApiDataService.GetProfileFromStringCallAsync(token);
            var xuid = profile["userXuid"].ToString();

            // Debug Profile response
            Console.WriteLine(profile);

            // Debug Xuid response
            Console.WriteLine("Xuid: " + xuid);

            return xuid;
        }
    }
}
