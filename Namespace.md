Namespace

    Mopidy
      |
      +-- Core
      |      |
      |      +-- Core
      |      |     |
      |      |     +-- Task<(bool Succeeded, string[] Result)> GetUriSchemes()
      |      |     +-- Task<(bool Succeeded, string Result)> GetVersion()
      |      |
      |      +-- Tracklist
      |      |     |
      |      |     +-- Criteria
      |      |     |
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Add(string[] uris, int? atPosition = null)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Add(string uri, int? atPosition = null)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Remove(int[] tlId)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Remove(string[] uri)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Remove(int tlId)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Remove(string uri)
      |      |     +-- Task<bool> Clear()
      |      |     +-- Task<bool> Move(int start, int end, int toPosition)
      |      |     +-- Task<bool> Shuffle(int? start = null, int? end = null)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> GetTlTracks()
      |      |     +-- Task<(bool Succeeded, int? Result)> Index(int tlId)
      |      |     +-- Task<(bool Succeeded, int Result)> GetVersion()
      |      |     +-- Task<(bool Succeeded, int Result)> GetLength()
      |      |     +-- Task<(bool Succeeded, Track[] Result)> GetTracks()
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Slice(int start, int end)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Filter(Criteria criteria)
      |      |     +-- Task<(bool Succeeded, TlTrack[] Result)> Filter(int? tlId = null, string uri = null)
      |      |     +-- Task<(bool Succeeded, int? Result)> GetEotTlId()
      |      |     +-- Task<(bool Succeeded, int? Result)> GetNextTlId()
      |      |     +-- Task<(bool Succeeded, int? Result)> GetPreviousTlId()
      |      |     +-- Task<(bool Succeeded, bool Result)> GetConsume()
      |      |     +-- Task<bool> SetConsume(bool value)
      |      |     +-- Task<(bool Succeeded, bool Result)> GetRandom()
      |      |     +-- Task<bool> SetRandom(bool value)
      |      |     +-- Task<(bool Succeeded, bool Result)> GetRepeat()
      |      |     +-- Task<bool> SetRepeat(bool value)
      |      |     +-- Task<(bool Succeeded, bool Result)> GetSingle()
      |      |     +-- Task<bool> SetSingle(bool value)
      |      |
      |      +-- Playback
      |      |     |
      |      |     +-- PlaybackState
      |      |     |
      |      |     +-- Task<bool> Play(int? tlId = null)
      |      |     +-- Task<bool> Next()
      |      |     +-- Task<bool> Previous()
      |      |     +-- Task<bool> Stop()
      |      |     +-- Task<bool> Pause()
      |      |     +-- Task<bool> Resume()
      |      |     +-- Task<(bool Succeeded, bool Result)> Seek(int timePosition)
      |      |     +-- Task<(bool Succeeded, TlTrack Result)> GetCurrentTlTrack()
      |      |     +-- Task<(bool Succeeded, Track Result)> GetCurrentTrack()
      |      |     +-- Task<(bool Succeeded, string Result)> GetStreamTitle()
      |      |     +-- Task<(bool Succeeded, int Result)> GetTimePosition()
      |      |     +-- Task<(bool Succeeded, PlaybackState Result)> GetState()
      |      |     +-- Task<bool> SetState(PlaybackState state)
      |      |
      |      +-- Library
      |      |     |
      |      |     +-- Query
      |      |     |
      |      |     +-- Task<(bool Succeeded, Ref[] Result)> Browse(string uri)
      |      |     +-- Task<(bool Succeeded, SearchResult[] Result)> Search(Query query, string[] uris = null, bool exact = false)
      |      |     +-- Task<(bool Succeeded, SearchResult[] Result)> Search(
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
      |      |     +-- Task<(bool Succeeded, SearchResult[] Result)> Search(
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
      |      |     +-- Task<(bool Succeeded, Dictionary<string, Track[]> Result)> Lookup(string[] uris)
      |      |     +-- Task<(bool Succeeded, Dictionary<string, Track[]> Result)> Lookup(string uris)
      |      |     +-- Task<bool> Refresh(string uri = null)
      |      |     +-- Task<(bool Succeeded, Dictionary<string, Image[]> Result)> GetImages(string[] uris)
      |      |     +-- Task<(bool Succeeded, Image[] Result)> GetImages(string uri)
      |      |
      |      +-- Playlists
      |      |     |
      |      |     +-- Task<(bool Succeeded, string[] Result)> GetUriSchemes()
      |      |     +-- Task<(bool Succeeded, Ref[] Result)> AsList()
      |      |     +-- Task<(bool Succeeded, Ref[] Result)> GetItems(string uri)
      |      |     +-- Task<(bool Succeeded, Playlist Result)> Lookup(string uri)
      |      |     +-- Task<bool> Refresh(string uriScheme = null)
      |      |     +-- Task<(bool Succeeded, Playlist Result)> Create(string name, string uriScheme = null)
      |      |     +-- Task<(bool Succeeded, Playlist Result)> Save(Playlist playlist)
      |      |     +-- Task<(bool Succeeded, bool Result)> Delete(string uri)
      |      |
      |      +-- Mixer
      |      |     |
      |      |     +-- Task<(bool Succeeded, bool Result)> GetMute()
      |      |     +-- Task<(bool Succeeded, bool Result)> SetMute(bool mute)
      |      |     +-- Task<(bool Succeeded, int? Result)> GetVolume()
      |      |     +-- Task<(bool Succeeded, bool Result)> SetVolume(int volume)
      |      |
      |      +-- History
      |      |     |
      |      |     +-- Task<(bool Succeeded, Dictionary<long, Ref> Result)> GetHistory()
      |      |     +-- Task<(bool Succeeded, int Result)> GetLength()
      |      |
      |
      +-- Models
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
      |     |     +-- RefType
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
      |     +-- Task<System.Drawing.Image> GetNative(Mopidy.Models.Image image)
      |
      +-- Settings
            |
            +-- Protocol
            |
            +-- Protocol ServerProtocol
            +-- string ServerAddress
            +-- int Port
            +-- readonly string BaseUrl
            +-- readonly string RpcUrl