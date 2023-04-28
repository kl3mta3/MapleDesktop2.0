using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode.Search;
using YoutubeExplode.Videos.Streams;

namespace MapleDesktop2._0
{
    internal class MusicSystem
    {

        //private List<string> searchResults = new List<string>();
        private List<VideoSearchResult> searchResults = new List<VideoSearchResult>();
        internal static List<Song> playlist = new List<Song>();
        internal List<Video> videoPlaylist = new List<Video>();
        internal int currentTrackPlaylistId = 0;
        internal int currentVideoPlaylistId = 0;
        internal int playlistCount = 0;
        internal string stroredRequest="";
        internal Song? currentTrack = new Song();

        internal Song urlDownloadResults = new Song();
        internal bool playingTrack = false;
        internal bool trackPaused = false;
        internal int selectedPlaylistTrack = 0;
        internal int currentVideoNumber = 0;
        internal int videoPlaylistCount = 0;
        internal Video? currentVideo = new Video();
        internal bool playingVideo = false;
        internal bool videoPaused = false;
        internal bool userPressedNext = false;
        internal bool userPressedStop = false;
        internal bool userpressedSeek = false;
        internal bool userpressedGo = false;
        internal bool userPressedLast = false;
        internal bool keepCurrentTrack = false;
        internal int currentTrackCurrentTime;
        internal double progressBarCurrentTime;
        private static string numberPattern = " ({0})";
        internal static bool saveMusic = false;
        public string? currentTrackProgress { get; set; } = "0:00/0:00";
        public string? currentTrackProgressstar { get; set; } = "0:00";


        internal void HandleRequest(string request, object sender, EventArgs e)
        {

            PlaySearchRequest(request, sender, e);

        }
        internal async Task PlaySearchRequest(string request, object sender, EventArgs e)
        {

            MainWindow.currentMusicForm.WriteToDebugConsole($"Sent Request for '{request}'");
            stroredRequest = request;
            await AddFileToPlaylist(request);


            if (MainWindow.playAudio || MainWindow.playVideo)
            {

                if (playingTrack)
                {
                    return;
                }
                if (currentTrack == null)
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Current Track is Empty looking for Track");
                    MainWindow.currentMusicForm.WriteToDebugConsole($"currentTrackId is {currentTrackPlaylistId}");
                    int nextTrackNumber = currentTrackPlaylistId + 1;
                    MainWindow.currentMusicForm.WriteToDebugConsole($"NexyTrackID is {nextTrackNumber}");

                    foreach (Song track in playlist)
                    {

                        if (track.playlistId == nextTrackNumber)
                        {

                            currentTrack = track;
                            currentTrackPlaylistId = track.playlistId;
                            MainWindow.currentMusicForm.WriteToDebugConsole($"Track Found {track.title}");
                            break;
                        }

                    }

                }
                else if (currentTrack != null)
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Next Track exists  is {currentTrack.title}");

                    int nextTrackNumber = currentTrackPlaylistId + 1;
                    MainWindow.currentMusicForm.WriteToDebugConsole($"NexyTrackNumber is {nextTrackNumber}");

                    foreach (Song track in playlist)
                    {

                        if (track.playlistId == nextTrackNumber)
                        {

                            currentTrack = track;
                            currentTrackPlaylistId = track.playlistId;
                            MainWindow.currentMusicForm.WriteToDebugConsole($"Next Track Found {track.title}");
                            break;
                        }

                    }



                }




                MainWindow.currentMusicForm.WriteToDebugConsole($"currentTrack path = {currentTrack.path}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"starting Track");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song title ={currentTrack.title}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song Artist= {currentTrack.author}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song length = {currentTrack.length}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song url = {currentTrack.url}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"Audio Playback Request Sent");

                currentTrackPlaylistId = currentTrack.playlistId;
                if (MainWindow.playAudio)
                {
                    PlaySong(currentTrack.path, sender, e);

                }
                else if (MainWindow.playVideo)
                {
                    if (playingVideo)
                    {
                        return;
                    }
                    if (!playingVideo)
                    {

                        MainWindow.currentMusicForm.WriteToDebugConsole($"Url sent for Playback {currentTrack.url}");
                        MainWindow.currentMusicForm.PlayVideoInWindow(currentTrack.url);
                    }
                }
            }


        }

        
        private void PlaySong(string filePath, object sender, EventArgs e)
        {

            MainWindow.currentMusicForm.WriteToDebugConsole($"Audio Playback Request Recieved for {currentTrack.title}");

            MainWindow.currentMusicForm.WriteToDebugConsole($"CurrentTrack is {currentTrack.title}");
            //MainWindow.currentMusicForm.DisplayCurrentSongInfo(currentTrack.title, currentTrack.author, currentTrack.url, currentTrack.playlistId.ToString());

            string songLength = currentTrack.length;
            string currentprogress = $"0:00/{currentTrack.length}";

            MainWindow.musicForm.SetProgressBarCurrentTrackLength(songLength);
            MainWindow.currentMusicForm.SetProgressBarCurrentProgress(currentprogress);
            MainWindow.currentMusicForm.WriteToDebugConsole($"Beging playback for {currentTrack.title} request sent");
            MainWindow.currentMusicForm.BeginPlayback(filePath);

        }

        internal void BeginVideoPlayback(string filePath)
        {
            MainWindow.currentMusicForm.WriteToDebugConsole("Video Begin Playback Triggered");

            MainWindow.currentMusicForm.PlayVideoInWindow(filePath);


        }



        internal void DeleteNonSavedFiles()
        {
            MainWindow.currentMusicForm.WriteToDebugConsole($"File Deletion entered with playlist size {playlist.Count}");
            foreach (Song song in playlist)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole($"title -{song.title}");
                if (!song.keepFile)
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"keepFile -{song.keepFile}");
                    try
                    {
                        File.Delete(song.path);
                        MainWindow.currentMusicForm.WriteToDebugConsole($"File Removed");
                    }
                    catch (Exception ex)
                    {
                        MainWindow.currentMusicForm.WriteToDebugConsole("File Delete Failed " + ex.Message);
                    }
                }

            }

        }



        private void PlayVideo(string filePath, object sender, EventArgs e)
        {


            MainWindow.currentMusicForm.WriteToDebugConsole($"Play Video Triggered with filepath: {filePath}");
            MainWindow.music.BeginVideoPlayback(filePath);
        }

        //internal Song SongFromUrl(string url)
        //{
        //    Song song = new Song();
        //    DownloadAudioFromURL(url);

        //    song = urlDownloadResults;
        //    MainWindow.currentMusicForm.WriteToDebugConsole($"Song {song.title} returned from url {url}");
        //    return song;


        //}

        //internal async Task DownloadAudioFromURL(string url)
        //{
        //    string result = url;
        //    MainWindow.currentMusicForm.WriteToDebugConsole($"Search Results= {result}");
        //    if (result == null)
        //    {

        //        MainWindow.currentMusicForm.WriteToDebugConsole("Error: Search Results Empty");
        //        return;

        //    }
        //    var youtube = MainWindow.youtube;
        //    var videoTitle = "";
        //    var videoAuthor = "";
        //    var videoDuration = "";
        //    var videoDataUrl = "";
        //    var videoUrl = result;
        //    var videoData = await youtube.Videos.GetAsync(videoUrl);
        //    string fileName = "";
        //    string musicFolder = "";
        //    string allegedPath = "";
        //    // string path = "";
        //    bool keepfile = false;
        //    // MapleHome.debugConsole.WriteToDebugConsole($"Extracting MetaData");

        //    videoTitle = videoData.Title;
        //    videoAuthor = videoData.Author.ChannelTitle;
        //    videoDuration = videoData.Duration.ToString();
        //    videoDataUrl = videoData.Url;
        //    MainWindow.currentMusicForm.WriteToDebugConsole($"Getting Audio Manifest for {videoTitle}");
        //    var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
        //    MainWindow.currentMusicForm.WriteToDebugConsole($"getting AudioStream");
        //    var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        //    MainWindow.currentMusicForm.WriteToDebugConsole($"Creating Audio Path");

        //    fileName = $"{videoData.Title} ";
        //    musicFolder = MainWindow.musicSavePath;
        //    allegedPath = System.IO.Path.Combine(musicFolder, $"{fileName}.{streamInfo.Container}");

        //    //filepath = NextAvailableFilename(allegedPath);

        //    if (!File.Exists(allegedPath))
        //    {
        //        MainWindow.currentMusicForm.WriteToDebugConsole($"Downloading Audio File");

        //        await youtube.Videos.Streams.DownloadAsync(streamInfo, allegedPath);

        //        if (saveMusic)
        //        {

        //            keepfile = true;
        //        }
        //        else
        //        {
        //            keepfile = false;

        //        }
        //        MainWindow.currentMusicForm.WriteToDebugConsole($"Audio File Download Done");

        //    }
        //    else
        //    {
        //        keepfile = true;
        //        MainWindow.currentMusicForm.WriteToDebugConsole($"Audio File Already Exists");
        //    }

        //    Song song = new Song();
        //    song.author = videoAuthor;
        //    song.title = videoTitle;
        //    song.length = videoDuration;
        //    song.url = videoDataUrl;
        //    song.path = allegedPath;
        //    song.keepFile = keepfile;

        //    urlDownloadResults = song;
        //}
        private async Task AddFileToPlaylist(string request)
        {

            await SearchForVideo(request);
            string result = searchResults[0].Url;
            MainWindow.currentMusicForm.WriteToDebugConsole($"Search Results= {result}");
            if (result == null)
            {

                MainWindow.currentMusicForm.WriteToDebugConsole("Error: Search Results Empty");
                return;

            }
            var youtube = MainWindow.youtube;
            var videoTitle = "";
            var videoAuthor = "";
            var videoDuration = "";
            var videoDataUrl = "";
            var videoUrl = result;
            var videoData = searchResults[0];
            string fileName = "";
            string musicFolder = "";
            string allegedPath = "";
            string allegedVideoPath = "";

            bool keepfile = false;
            MainWindow.currentMusicForm.WriteToDebugConsole($"Extracting MetaData");



            videoTitle = videoData.Title;
            videoAuthor = videoData.Author.ChannelTitle;
            videoDuration = videoData.Duration.ToString();
            videoDataUrl = videoData.Url;
            MainWindow.currentMusicForm.WriteToDebugConsole($"Getting Audio Manifest for {videoTitle}");
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            MainWindow.currentMusicForm.WriteToDebugConsole($"getting AudioStream");
            var  streamInfo = streamManifest.GetAudioOnlyStreams().TryGetWithHighestBitrate();
            
            MainWindow.currentMusicForm.WriteToDebugConsole($"Creating Audio File Name");

            MainWindow.currentMusicForm.WriteToDebugConsole($"Base Name = {videoData.Title}");
            fileName = $"{videoData.Title}";
            musicFolder = MainWindow.musicSavePath;
            MainWindow.currentMusicForm.WriteToDebugConsole($"Using File Name {fileName}.{streamInfo.Container}");
                string second = @"\"+stroredRequest+"."+streamInfo.Container;
            MainWindow.currentMusicForm.WriteToDebugConsole("alleged path is "+musicFolder+@"\"+fileName+"."+streamInfo.Container);
            MainWindow.currentMusicForm.WriteToDebugConsole("alt path is " + musicFolder + @"\" + second);
            try
            {
                MainWindow.currentMusicForm.WriteToDebugConsole("Trying alleged path");
            allegedPath = Path.Combine(musicFolder, $"{fileName}.{streamInfo.Container}");

            }
            catch (Exception ex)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole("Main path failed using backup");
                MainWindow.currentMusicForm.WriteToDebugConsole( ex.Message);
                allegedPath = musicFolder + second;
            }

            //allegedPath = musicFolder+@"\"+fileName+"."+streamInfo.Container;
            MainWindow.currentMusicForm.WriteToDebugConsole($"File {videoData.Title} created at {allegedPath}");
            if (MainWindow.playAudio || MainWindow.saveMusic || MainWindow.playVideo)
            {



                if (!File.Exists(allegedPath))
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Downloading Audio File");
                    try
                    {
                        await youtube.Videos.Streams.DownloadAsync(streamInfo, allegedPath);

                    }
                    catch (Exception ex)
                    {

                        MainWindow.currentMusicForm.WriteToDebugConsole(ex.Message);
                    }

                    if (MainWindow.saveMusic)
                    {

                        keepfile = true;
                    }
                    else
                    {
                        keepfile = false;

                    }
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Audio File Download Done");

                }
                else
                {
                    keepfile = true;
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Audio File Already Exists");
                }

            }
            if (MainWindow.playAudio || MainWindow.playVideo)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole($"Playing in Audio Mode");
                Song song = new Song();
                song.author = videoAuthor;
                song.title = videoTitle;
                song.length = videoDuration;
                song.url = videoDataUrl;
                song.path = allegedPath;
                song.keepFile = keepfile;


                MainWindow.currentMusicForm.WriteToDebugConsole($"song.title -{song.title}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song.author-{song.author}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song.length-{song.length}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song.url-{song.url}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song.path -{song.path}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"song.keepFile-{song.keepFile}");






                int playlistid = playlistCount + 1;
                playlistCount++;

                MainWindow.currentMusicForm.WriteToDebugConsole($"playlistid-{playlistid}");
                song.playlistId = playlistid;

                playlist.Add(song);
                stroredRequest = "";
                MainWindow.music.DisplayAudioPlaylist();
                MainWindow.currentMusicForm.WriteToDebugConsole($" {song.title} added to Audio Playlist ");
                MainWindow.currentMusicForm.WriteToDebugConsole($" playlist count {playlist.Count}");


                if (MainWindow.saveVideo) /// for video playback
                {

                    MainWindow.currentMusicForm.WriteToDebugConsole($"Video Save triggered");

                    videoData = searchResults[0];
                    videoTitle = videoData.Title;
                    videoAuthor = videoData.Author.ChannelTitle;
                    videoDuration = videoData.Duration.ToString();
                    videoDataUrl = videoData.Url;
                    streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Getting Streams");
                    try
                    {
                        streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();



                        fileName = $"{videoData.Title} by {videoData.Author.ChannelTitle} ";
                        MainWindow.currentMusicForm.WriteToDebugConsole($"Video {videoData.Title} set for download");

                        var videoFolder = MainWindow.videoSavePath;
                        allegedVideoPath = System.IO.Path.Combine(videoFolder, $"{fileName}.{streamInfo.Container}");
                        MainWindow.currentMusicForm.WriteToDebugConsole($"alleged {allegedVideoPath} set for video download");


                        if (!File.Exists(allegedVideoPath))
                        {
                            await youtube.Videos.Streams.DownloadAsync(streamInfo, allegedVideoPath);
                            MainWindow.currentMusicForm.WriteToDebugConsole($"Video {videoData.Title} downloaded");



                        }
                        else
                        {
                            MainWindow.currentMusicForm.WriteToDebugConsole($"Video {videoData.Title} Already Exists");
                        }
                    }
                    catch (Exception ex)
                    {
                        MainWindow.currentMusicForm.WriteToDebugConsole($"Video {videoData.Title} download failed");
                        MainWindow.currentMusicForm.WriteToDebugConsole($"Error {ex.Message}");
                    }

                }
            }
        }
        //if (MainWindow.playVideo)
        //{
        //    Video video = new Video();
        //    video.author = videoAuthor;
        //    video.title = videoTitle;
        //    video.length = videoDuration;
        //    video.url = videoDataUrl;
        //    if (allegedVideoPath != null)
        //    {
        //        video.path = allegedVideoPath;

        //    }

        //    int playlistid = videoPlaylistCount + 1;
        //    videoPlaylistCount++;
        //    video.playlistId = playlistid;

        //    videoPlaylist.Add(video);

        //    //MainWindow.music.DisplayVideoPlaylist();
        //    MainWindow.currentMusicForm.WriteToDebugConsole($" {video.title} added to Video Playlist ");
        //    MainWindow.currentMusicForm.WriteToDebugConsole($" Video playlist count {playlist.Count}");

        //} ///for video Playback  



        internal void DisplayAudioPlaylist()
        {

            MainWindow.currentMusicForm.ClearPlaylistConsole();

            List<Song> orderedPlaylist = playlist.OrderBy(i => i.playlistId).ToList();

            foreach (Song song in orderedPlaylist)
            {

                MainWindow.currentMusicForm.WriteToPlaylistConsole($"{song.playlistId}. {song.title}");


            }


        }

        internal void DisplayVideoPlaylist()
        {
            ;

            List<Video> orderedPlaylist = videoPlaylist.OrderBy(i => i.playlistId).ToList();


            foreach (Video entry in videoPlaylist)
            {
                // do something with entry.Value or entry.Key

                // MainWindow.playlistConsole.WriteToPlaylistConsole($"Video: {entry.playlistId}. {entry.title}");
            }



        }

        internal void PlayNextTrack(object sender, EventArgs e)
        {
            MainWindow.currentMusicForm.WriteToDebugConsole($"Next Track Process started");

            if (playlist.Count <= 0)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole($"No songs in playlist");
                return;
            }
            MainWindow.currentMusicForm.WriteToDebugConsole($"Searching for next Track");
            Song track = SearchForNextTrack();

            MainWindow.currentMusicForm.WriteToDebugConsole($"Next Track Found....Audio Playback Request for {track.title} Sent");
            currentTrack = track;
            currentTrackPlaylistId = track.playlistId;

            PlaySong(track.path, sender, e);



        }



        internal void PlayPlaylistTrack(object sender, EventArgs e)
        {
            MainWindow.currentMusicForm.WriteToDebugConsole($"PlayList Track Process started");
            var trackNumber = selectedPlaylistTrack;
            MainWindow.currentMusicForm.WriteToDebugConsole($"PlayList Count {playlist.Count}");
            if (playlist.Count <= 0)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole($"No songs in playlist");
                return;
            }
            if (trackNumber <= playlist.Count)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole($"Searching for Track by ID  {trackNumber}");
                Song track = SearchForPlaylistTrack(trackNumber);

                //MainWindow.currentMusicForm.WriteToDebugConsole($"Next Track Found....Audio Playback Request for {track.title} Sent");
                currentTrack = track;
                currentTrackPlaylistId = track.playlistId;
                MainWindow.currentMusicForm.WriteToDebugConsole($"CurrentTrack is  {currentTrack.title}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"CurrentTrack artist is  {currentTrack.author}");
                MainWindow.currentMusicForm.WriteToDebugConsole($"CurrentTrack id is  {currentTrack.playlistId}");
                PlaySong(track.path, sender, e);
                selectedPlaylistTrack = 0;
            }

        }

        internal Song SearchForPlaylistTrack(int trackNumber)
        {
            int nextTrackid = trackNumber;
            Song song = new Song();

            //nextTrackid = trackNumber;
            foreach (Song track in playlist)
            {
                if (track.playlistId == nextTrackid)
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Next song is ID {track.playlistId}");

                    return track;

                }
            }

            MainWindow.currentMusicForm.WriteToDebugConsole($"No Next Track Found");
            return song;

        }

        internal Song SearchForNextTrack()
        {
            int nextTrackid = 1;
            Song song = new Song();
            if (currentTrackPlaylistId >= playlist.Count)
            {
                nextTrackid = 1;
                MainWindow.currentMusicForm.WriteToDebugConsole($"At end of playlist starting to Start");
            }
            else
            {
                nextTrackid = currentTrackPlaylistId + 1;
                MainWindow.currentMusicForm.WriteToDebugConsole($"Not at end of play list");

            }

            foreach (Song track in playlist)
            {
                if (track.playlistId == nextTrackid)
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"Next song is {track.title}");
                    currentTrack = track;
                    currentTrackPlaylistId = track.playlistId;
                    return track;

                }
            }

            MainWindow.currentMusicForm.WriteToDebugConsole($"No Next Track Found");
            return song;

        }



        internal Song SearchForLastTrack()
        {
            int nextTrackid = 0;
            if (currentTrackPlaylistId <= 1)
            {
                nextTrackid = playlist.Count;


            }
            else
            {

                nextTrackid = currentTrackPlaylistId - 1;


            }
            foreach (Song track in playlist)
            {
                MainWindow.currentMusicForm.WriteToDebugConsole($"Checking song with {track.playlistId} {track.title}");
                if (track.playlistId == nextTrackid)
                {
                    currentTrack = track;
                    currentTrackPlaylistId = track.playlistId;
                    return track;

                }
            }
            MainWindow.currentMusicForm.WriteToDebugConsole($"No Next Track Found");
            return null;

        }
        internal void PlayLastTrack(object sender, EventArgs e)
        {
            try
            {

                MainWindow.currentMusicForm.WriteToDebugConsole($"Last Track Process started");

                if (playlist.Count <= 1)
                {
                    MainWindow.currentMusicForm.WriteToDebugConsole($"No songs in playlist");
                    return;
                }
                MainWindow.currentMusicForm.WriteToDebugConsole($"Searching for Last Track");
                Song track = SearchForLastTrack();
                //if (!File.Exists(track.path))
                //{

                //    DownloadAudioFromURL(track.url);
                //}

                MainWindow.currentMusicForm.WriteToDebugConsole($"Last Track Found....Audio Playback Request for {track.title} Sent");
                currentTrack = track;
                currentTrackPlaylistId = track.playlistId;
                //keepCurrentTrack = track.keepFile;
                PlaySong(track.path, sender, e);
            }
            catch (Exception ex)
            {

                MainWindow.currentMusicForm.WriteToDebugConsole($"Error in PlayPreviousTrack {ex.Message}");


            }



        }


        internal void ClearAudioPlaylist()
        {

            DeleteNonSavedFiles();
            playlist.Clear();
            playlistCount = 0;
            currentTrackPlaylistId = 0;
            currentTrack = null;
            MainWindow.currentMusicForm.WriteToDebugConsole($"Playlist Cleared");
        }


        private async Task SearchForVideo(string request)
        {



            MainWindow.debugConsole.WriteToDebugConsole($"Searching for '{request}'");


            if (searchResults.Count > 0)
            {
                searchResults.Clear();
            }


            await foreach (var video in MainWindow.youtube.Search.GetVideosAsync(request))
            {

                if (searchResults.Count < 1)
                {

                    searchResults.Add(video);
                    MainWindow.debugConsole.WriteToDebugConsole($"{video.Title} added to searchresults");
                    break;

                }





            }



        }

        private void ClearSearchResults()
        {

            searchResults.Clear();
        }

        public static string NextAvailableFilename(string path)
        {

            if (!File.Exists(path))
            {


                return path;
            }

            // If path has extension then insert the number pattern just before the extension and return next filename
            else if (System.IO.Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(System.IO.Path.GetExtension(path)), numberPattern));

            else
            {
                // helper.WriteToMapleConsole($"Renaming file");
                return GetNextFilename(path + numberPattern);
            }

        }


        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }
        public class Video
        {
            public string? title { get; set; }
            public string? length { get; set; }
            public string? author { get; set; }
            public string? url { get; set; }
            public string? path { get; set; }
            public int playlistId { get; set; }

        }

        public class Song
        {

            public string? title { get; set; }
            public string? length { get; set; }
            public string? author { get; set; }
            public string? url { get; set; }
            public string? path { get; set; }

            public bool keepFile { get; set; }
            public int playlistId { get; set; }

        }












    }
}
