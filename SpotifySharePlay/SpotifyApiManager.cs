using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SpotifySharePlay {
    /// <summary>
    /// Controller class for the Spotify player
    /// </summary>
    public class SpotifyApiManager {
        public delegate void AuthCompletedHandler();
        public AuthCompletedHandler AuthCompleted;

        public string clientId = "7984f21178dd421587060d3fe281f79d";
        private SpotifyWebAPI spotify;
        ImplictGrantAuth auth;

        public SpotifyWebAPI Spotify {
            get {
                return spotify;
            }
        }

        public SpotifyApiManager() {
           
        }

        public void Init() {
            auth = new ImplictGrantAuth(clientId, "http://localhost:8000", "http://localhost:8000");
            auth.Scope = Scope.UserModifyPlaybackState | Scope.UserReadPlaybackState;

            //Start the internal http server
            auth.Start();
            auth.OpenBrowser();

            auth.AuthReceived += (obj, token) => {
                spotify = new SpotifyWebAPI();
                spotify.AccessToken = token.AccessToken;
                spotify.TokenType = token.TokenType;

                auth.Stop();

                AuthCompleted();
            };
        }

        public void Update(PlaybackContext playback) {
            /// Play or pause according to the playback info
            if (playback == null || playback.Item == null)
                return;

            if (playback.IsPlaying) {
                spotify.ResumePlayback(uris: new List<string>() { playback.Item.Uri }, offset: (int?)null);
                spotify.SeekPlayback(playback.ProgressMs);
            } else {
                spotify.PausePlayback();
            }
        }
    }
}
