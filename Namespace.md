## Namespace Tree

    Mopidy
      |
      +-- Core
      |      |
      |      +-- Core
      |      |     |
      |      |     +-- static Task<(bool Succeeded, string[] Result)> GetUriSchemes()
      |      |     +-- static Task<(bool Succeeded, string Result)> GetVersion()
      |      |
      |      +-- Tracklist
      |      |     |
      |      |     +-- Criteria
      |      |     |
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Add(string[] uris, int? atPosition = null)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Add(string uri, int? atPosition = null)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Remove(int[] tlId)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Remove(string[] uri)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Remove(int tlId)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Remove(string uri)
      |      |     +-- static Task<bool> Clear()
      |      |     +-- static Task<bool> Move(int start, int end, int toPosition)
      |      |     +-- static Task<bool> Shuffle(int? start = null, int? end = null)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> GetTlTracks()
      |      |     +-- static Task<(bool Succeeded, int? Result)> Index(int tlId)
      |      |     +-- static Task<(bool Succeeded, int Result)> GetVersion()
      |      |     +-- static Task<(bool Succeeded, int Result)> GetLength()
      |      |     +-- static Task<(bool Succeeded, Track[] Result)> GetTracks()
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Slice(int start, int end)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Filter(Criteria criteria)
      |      |     +-- static Task<(bool Succeeded, TlTrack[] Result)> Filter(int? tlId = null, string uri = null)
      |      |     +-- static Task<(bool Succeeded, int? Result)> GetEotTlId()
      |      |     +-- static Task<(bool Succeeded, int? Result)> GetNextTlId()
      |      |     +-- static Task<(bool Succeeded, int? Result)> GetPreviousTlId()
      |      |     +-- static Task<(bool Succeeded, bool Result)> GetConsume()
      |      |     +-- static Task<bool> SetConsume(bool value)
      |      |     +-- static Task<(bool Succeeded, bool Result)> GetRandom()
      |      |     +-- static Task<bool> SetRandom(bool value)
      |      |     +-- static Task<(bool Succeeded, bool Result)> GetRepeat()
      |      |     +-- static Task<bool> SetRepeat(bool value)
      |      |     +-- static Task<(bool Succeeded, bool Result)> GetSingle()
      |      |     +-- static Task<bool> SetSingle(bool value)
      |      |
      |      +-- Playback
      |      |     |
      |      |     +-- static Task<bool> Play(int? tlId = null)
      |      |     +-- static Task<bool> Next()
      |      |     +-- static Task<bool> Previous()
      |      |     +-- static Task<bool> Stop()
      |      |     +-- static Task<bool> Pause()
      |      |     +-- static Task<bool> Resume()
      |      |     +-- static Task<(bool Succeeded, bool Result)> Seek(int timePosition)
      |      |     +-- static Task<(bool Succeeded, TlTrack Result)> GetCurrentTlTrack()
      |      |     +-- static Task<(bool Succeeded, Track Result)> GetCurrentTrack()
      |      |     +-- static Task<(bool Succeeded, string Result)> GetStreamTitle()
      |      |     +-- static Task<(bool Succeeded, int Result)> GetTimePosition()
      |      |     +-- static Task<(bool Succeeded, PlaybackState Result)> GetState()
      |      |     +-- static Task<bool> SetState(PlaybackState state)
      |      |
      |      +-- Library
      |      |     |
      |      |     +-- Query
      |      |     |
      |      |     +-- static Task<(bool Succeeded, Ref[] Result)> Browse(string uri)
      |      |     +-- static Task<(bool Succeeded, SearchResult[] Result)> Search(Query query, string[] uris = null, bool exact = false)
      |      |     +-- static Task<(bool Succeeded, SearchResult[] Result)> Search(
      |      |     |       string[] queryUri = null,
      |      |     |       string[] queryTrackName = null,
      |      |     |       string[] queryAlbum = null,
      |      |     |       string[] queryArtist = null,
      |      |     |       string[] queryAlbumArtist = null,
      |      |     |       string[] queryComposer = null,
      |      |     |       string[] queryPerformer = null,
      |      |     |       int[] queryTrackNo = null,
      |      |     |       string[] queryGenre = null,
      |      |     |       string[] queryDate = null,
      |      |     |       string[] queryComment = null,
      |      |     |       string[] queryAny = null,
      |      |     |       string[] uris = null,
      |      |     |       bool exact = false
      |      |     |   )
      |      |     |
      |      |     +-- static Task<(bool Succeeded, SearchResult[] Result)> Search(
      |      |     |       string queryUri = null,
      |      |     |       string queryTrackName = null,
      |      |     |       string queryAlbum = null,
      |      |     |       string queryArtist = null,
      |      |     |       string queryAlbumArtist = null,
      |      |     |       string queryComposer = null,
      |      |     |       string queryPerformer = null,
      |      |     |       int? queryTrackNo = null,
      |      |     |       string queryGenre = null,
      |      |     |       string queryDate = null,
      |      |     |       string queryComment = null,
      |      |     |       string queryAny = null,
      |      |     |       string[] uris = null,
      |      |     |       bool exact = false
      |      |     |   )
      |      |     |
      |      |     +-- static Task<(bool Succeeded, Dictionary<string, Track[]> Result)> Lookup(string[] uris)
      |      |     +-- static Task<(bool Succeeded, Dictionary<string, Track[]> Result)> Lookup(string uris)
      |      |     +-- static Task<bool> Refresh(string uri = null)
      |      |     +-- static Task<(bool Succeeded, Dictionary<string, Image[]> Result)> GetImages(string[] uris)
      |      |     +-- static Task<(bool Succeeded, Image[] Result)> GetImages(string uri)
      |      |
      |      +-- Playlists
      |      |     |
      |      |     +-- static Task<(bool Succeeded, string[] Result)> GetUriSchemes()
      |      |     +-- static Task<(bool Succeeded, Ref[] Result)> AsList()
      |      |     +-- static Task<(bool Succeeded, Ref[] Result)> GetItems(string uri)
      |      |     +-- static Task<(bool Succeeded, Playlist Result)> Lookup(string uri)
      |      |     +-- static Task<bool> Refresh(string uriScheme = null)
      |      |     +-- static Task<(bool Succeeded, Playlist Result)> Create(string name, string uriScheme = null)
      |      |     +-- static Task<(bool Succeeded, Playlist Result)> Save(Playlist playlist)
      |      |     +-- static Task<(bool Succeeded, bool Result)> Delete(string uri)
      |      |
      |      +-- Mixer
      |      |     |
      |      |     +-- static Task<(bool Succeeded, bool Result)> GetMute()
      |      |     +-- static Task<(bool Succeeded, bool Result)> SetMute(bool mute)
      |      |     +-- static Task<(bool Succeeded, int? Result)> GetVolume()
      |      |     +-- static Task<(bool Succeeded, bool Result)> SetVolume(int volume)
      |      |
      |      +-- History
      |      |     |
      |      |     +-- static Task<(bool Succeeded, Dictionary<long, Ref> Result)> GetHistory()
      |      |     +-- static Task<(bool Succeeded, int Result)> GetLength()
      |      |
      |      +-- CoreListener
      |            |
      |            +-- static EventHandler<MuteChangedEventArgs> MuteChanged
      |            +-- static EventHandler<OptionsChangedEventArgs> OptionsChanged
      |            +-- static EventHandler<PlaybackStateChangedEventArgs> PlaybackStateChanged
      |            +-- static EventHandler<PlaylistChangedEventArgs> PlaylistChanged
      |            +-- static EventHandler<PlaylistDeletedEventArgs> PlaylistDeleted
      |            +-- static EventHandler<PlaylistsLoadedEventArgs> PlaylistsLoaded
      |            +-- static EventHandler<SeekedEventArgs> Seeked
      |            +-- static EventHandler<StreamTitleChangedEventArgs> StreamTitleChanged
      |            +-- static EventHandler<TracklistChangedEventArgs> TracklistChanged
      |            +-- static EventHandler<TrackPlaybackEndedEventArgs> TrackPlaybackEnded
      |            +-- static EventHandler<TrackPlaybackPausedEventArgs> TrackPlaybackPaused
      |            +-- static EventHandler<TrackPlaybackResumedEventArgs> TrackPlaybackResumed
      |            +-- static EventHandler<TrackPlaybackStartedEventArgs> TrackPlaybackStarted
      |            +-- static EventHandler<VolumeChangedEventArgs> VolumeChanged
      |            +-- static EventHandler<UnexpectedEventEventArgs> UnexpectedEvent
      |
      +-- Models
      |     |
      |     +-- Enums
      |     |     |
      |     |     +-- enum PlaybackState
      |     |     +-- enum RefType
      |     |
      |     +-- EventArgs
      |     |     |
      |     |     +-- MuteChangedEventArgs
      |     |     |     |
      |     |     |     +-- bool Mute
      |     |     |
      |     |     +-- OptionsChangedEventArgs
      |     |     |
      |     |     +-- PlaybackStateChanged
      |     |     |     |
      |     |     |     +-- PlaybackState OldState
      |     |     |     +-- PlaybackState NewState
      |     |     |
      |     |     +-- PlaylistChangedEventArgs
      |     |     |     |
      |     |     |     +-- Playlist Playlist
      |     |     |
      |     |     +-- PlaylistDeletedEventArgs
      |     |     |     |
      |     |     |     +-- string Uri
      |     |     |
      |     |     +-- PlaylistsLoadedEventArgs
      |     |     |
      |     |     +-- SeekedEventArgs
      |     |     |     |
      |     |     |     +-- int TimePosition
      |     |     |
      |     |     +-- StreamTitleChangedEventArgs
      |     |     |     |
      |     |     |     +-- string Title
      |     |     |
      |     |     +-- TracklistChangedEventArgs
      |     |     |
      |     |     +-- TrackPlaybackEndedEventArgs
      |     |     |     |
      |     |     |     +-- TlTrack TlTrack
      |     |     |     +-- int TimePosition
      |     |     |
      |     |     +-- TrackPlaybackPausedEventArgs
      |     |     |     |
      |     |     |     +-- TlTrack TlTrack
      |     |     |     +-- int TimePosition
      |     |     |
      |     |     +-- TrackPlaybackResumedEventArgs
      |     |     |     |
      |     |     |     +-- TlTrack TlTrack
      |     |     |     +-- int TimePosition
      |     |     |
      |     |     +-- TrackPlaybackStartedEventArgs
      |     |     |     |
      |     |     |     +-- TlTrack TlTrack
      |     |     |
      |     |     +-- VolumeChangedEventArgs
      |     |     |     |
      |     |     |     +-- int Volume
      |     |     |
      |     |     +-- UnexpectedEventEventArgs
      |     |
      |     +-- Album
      |     |     |
      |     |     +-- Artist[] Artists
      |     |     +-- string Date
      |     |     +-- string MusicbrainzId
      |     |     +-- string Name
      |     |     +-- int? NumDiscs
      |     |     +-- int? NumTracks
      |     |     +-- string Uri
      |     |
      |     +-- Artist
      |     |     |
      |     |     +-- string MusicbrainzId
      |     |     +-- string Name
      |     |     +-- string SortName
      |     |     +-- string Uri
      |     |
      |     +-- Image
      |     |     |
      |     |     +-- string Uri
      |     |     +-- int? Height
      |     |     +-- int? Width
      |     |     +-- Task<System.Drawing.Image> GetNativeImage()
      |     |
      |     +-- Playlist
      |     |     |
      |     |     +-- long? LastModified
      |     |     +-- int? Length
      |     |     +-- string Name
      |     |     +-- List<Track> Tracks
      |     |     +-- string Uri
      |     |
      |     +-- Ref
      |     |     |
      |     |     +-- string Name
      |     |     +-- RefType Type
      |     |     +-- string Uri
      |     |
      |     +-- SearchResult
      |     |     |
      |     |     +-- Album[] Albums
      |     |     +-- Artist[] Artists
      |     |     +-- Track[] Tracks
      |     |     +-- string Uri
      |     |
      |     +-- TlTrack
      |     |     |
      |     |     +-- int TlId
      |     |     +-- Track Track
      |     |
      |     +-- Track
      |           |
      |           +-- Album Album
      |           +-- Artist[] Artists
      |           +-- int BitRate
      |           +-- string Comment
      |           +-- Artist[] Composers
      |           +-- string Date
      |           +-- int? DiscNo
      |           +-- string Genre
      |           +-- long? LastModified
      |           +-- int? Length
      |           +-- string MusicbrainzId
      |           +-- string Name
      |           +-- Artist[] Performers
      |           +-- int? TrackNo
      |           +-- string Uri
      |
      +-- Images
      |     |
      |     +-- static Task<System.Drawing.Image> GetNative(Mopidy.Models.Image image)
      |     +-- static Task<System.Drawing.Image> GetNative(string uri)
      |
      +-- Settings
            |
            +-- enum Connection
            +-- enum Encryption
            |
            +-- static Connection ConnectionType
            +-- static Encryption EncryptionType
            +-- static string ServerAddress
            +-- static int ServerPort
            +-- static readonly string BaseUrl
            +-- static readonly string RpcUrl
            +-- static readonly string WebSocketUrl
