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

            if (xuid != null)
            {
                var screenshots = await XboxApiDataService.GetScreenshotsFromStreamCallAsync(token, xuid);

                // Debug Screenshot response
                string jsonString = JsonConvert.SerializeObject(screenshots, Formatting.Indented);
                Console.WriteLine(jsonString);

                return screenshots;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<GameClip>> GetGameClips(CancellationToken token)
        {
            var xuid = await GetXuid(token);

            if(xuid != null)
            {
                var gameClips = await XboxApiDataService.GetGameClipsFromStreamCallAsync(token, xuid);

                // Debug GameClip response
                string jsonString = JsonConvert.SerializeObject(gameClips, Formatting.Indented);
                Console.WriteLine(jsonString);

                return gameClips;
            }
            else
            {
                return null;
            }
        }

        public static async Task<string> GetXuid(CancellationToken token)
        {
            var profile = await XboxApiDataService.GetProfileFromStringCallAsync(token);

            if (profile != null)
            {
                var xuid = profile["userXuid"].ToString();

                // Debug Profile response
                Console.WriteLine(profile);

                // Debug Xuid response
                Console.WriteLine("Xuid: " + xuid);

                return xuid;
            }
            else
            {
                return null;
            }
        }

        public static async Task<string> GetGamerTag(CancellationToken token)
        {
            var profile = await XboxApiDataService.GetProfileFromStringCallAsync(token);

            if (profile != null)
            {
                var gamerTag = profile["gamerTag"].ToString();

                // Debug Profile response
                Console.WriteLine(profile);

                // Debug Xuid response
                Console.WriteLine("Gamer Tag: " + gamerTag);

                return gamerTag;
            }
            else
            {
                return null;
            }
        }
    }
}
