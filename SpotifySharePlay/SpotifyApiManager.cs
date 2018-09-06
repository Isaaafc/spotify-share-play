using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySharePlay {
    class SpotifyApiManager {
        private ImplictGrantAuth auth;
        private string clientId = "7984f21178dd421587060d3fe281f79d";

        public SpotifyApiManager() {
           
        }

        public void Init() {
            auth = new ImplictGrantAuth(clientId, "http://localhost:8000", "http://localhost:8000", Scope.UserReadPrivate);

            //Start the internal http server
            auth.Start();
            auth.OpenBrowser();
        }
    }
}
