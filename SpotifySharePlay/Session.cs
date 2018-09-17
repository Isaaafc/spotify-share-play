using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpotifySharePlay {
    /// <summary>
    /// Maintains a session's variables and communicates with the server
    /// </summary>
    public class Session {
        private const string domain = "http://localhost", secret = "abcdef";
        public const int interval = 5000;

        private PlaybackContext playback;

        public PlaybackContext Playback {
            get {
                return playback;
            }

            set {
                if (value != null) {
                    /// Set PlaybackChanged only if track is different or difference between progress is > 30s or playback state changed
                    PlaybackChanged = (playback == null
                                        || playback.IsPlaying != value.IsPlaying
                                        || playback.Item.Uri != value.Item.Uri
                                        || Math.Abs(playback.Item.DurationMs - value.Item.DurationMs) > 30000);

                    playback = value;
                } else
                    PlaybackChanged = false;
            }
        }

        public bool PlaybackChanged { get; set; }

        public int Interval {
            get {
                return interval;
            }
        }

        /// <summary>
        /// For verification / privileges
        /// </summary>
        public Guid UserID { get; set; }

        private string key;
        private DownloadManager downloadManager;

        public string Key {
            get {
                return key;
            }

            set {
                if (ValidateKey(value))
                    key = value;
                else
                    key = null;
            }
        }
        
        public Session() {
            downloadManager = new DownloadManager(domain);
            UserID = new Guid();
        }

        /// <summary>
        /// Syncs the playback status with the server, prioritizes local changes
        /// </summary>
        public void Sync() {
            var pbParams = new Dictionary<string, string>();
            pbParams["secret"] = secret;
            pbParams["playback"] = JsonConvert.SerializeObject(playback);

            HttpWebRequest request = null;
            string json = downloadManager.PostRequest(String.Format("{0}/{1}/{2}", domain, "join", Key), pbParams, ref request);

            dynamic jObj = JObject.Parse(json);
            if (jObj.Success) {
                var serverPlayback = JsonConvert.DeserializeObject<PlaybackContext>(jObj.Playback);


            }

            if (PlaybackChanged) {
                /// If the track is changed locally, send a signal to the server to update its state
                
            } else {
                /// If the user have not changed the track, get from the server the status of the track playback
                
            }
        }

        public bool GenerateKey() {
            var pbParams = new Dictionary<string, string>();
            pbParams["secret"] = secret;

            HttpWebRequest request = null;
            Key = downloadManager.PostRequest("http://localhost:8080/create/", pbParams, ref request);

            Console.WriteLine(Key == null ? "null" : Key);
            return Key != null;
        }

        private bool ValidateKey(string val) {
            return true;
        }
    }
}
