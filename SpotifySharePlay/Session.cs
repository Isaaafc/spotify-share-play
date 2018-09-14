using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;

namespace SpotifySharePlay {
    /// <summary>
    /// Maintains a session's variables and communicates with the server
    /// </summary>
    public class Session {
        private const string domain = "http://localhost";
        public const int interval = 5000;

        private PlaybackContext playback;

        #region test
        public string jsonResponse;
        #endregion

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
            if (jsonResponse != null)
                Playback = Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyAPI.Web.Models.PlaybackContext>(jsonResponse);

            if (PlaybackChanged) {
                /// If the track is changed locally, send a signal to the server to update its state

            } else {
                /// If the user have not changed the track, get from the server the status of the track playback
                
            }
        }

        public bool GenerateKey() {
            Key = "asdfoijwef";

            return Key != null;
        }

        private bool ValidateKey(string val) {
            return true;
        }
    }
}
