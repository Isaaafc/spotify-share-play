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

        public SpotifyApiManager() {
           
        }

        public void Init() {
            auth = new ImplictGrantAuth(clientId, "http://localhost:8000", "http://localhost:8000");
            auth.Scope = Scope.UserModifyPlaybackState | Scope.Streaming;

            //Start the internal http server
            auth.Start();
            auth.OpenBrowser();

            auth.AuthReceived += (obj, token) => {
                spotify = new SpotifyWebAPI();
                spotify.AccessToken = token.AccessToken;
                spotify.TokenType = token.TokenType;

                Console.WriteLine("Received token: {0}, type: {1}", spotify.AccessToken, spotify.TokenType);
                auth.Stop();

                AuthCompleted();
            };
        }

        public void Play(string context, int offset = 0) {
            spotify.ResumePlayback(contextUri: context, offset: offset);
        }

        public void Pause() {
            spotify.PausePlayback();
        }

        public AvailabeDevices GetDevices() {
            return spotify.GetDevices();
        }
    }
}
