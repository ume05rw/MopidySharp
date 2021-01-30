MopidySharp - Library for Mopidy Music Server
====

Implemented all methods on mopidy.core.  
Connect by Http-Post/JSON-RPC, or WebSocket(limitted support).

## Description

Supports .NET Standard2.0

## Requirement

[Newtonsoft.Json ver >= 12.0.3](https://www.nuget.org/packages/Newtonsoft.Json/)  
[System.Drawing.Common ver >= 5.0.0](https://www.nuget.org/packages/System.Drawing.Common/)  

## Usage

1. [Add NuGet-Package](https://www.nuget.org/packages/MopidySharp/) to your project.  
1. Write Code Like:

```
// using Mopidy.Core;

Mopidy.Settings.ServerAddress = "Set your Mopidy IP-Address";
var res = await Tracklist.GetTlTracks();

if (res.Succeeded)
    foreach (var tlTrack in res.Result)
        System.Diagnostics.Debug.WriteLine(tlTrack.Track.Name);
```

Node:  
The default setting is to connect by Http-Post.  
In this case, ``Mopidy.Core.CoreListener`` does not work.  
It is necessary to intentionally acquire and update the state by polling or other means.  

#### Added limitted support for WebSocket on ver 1.1.0.  

Retrieving event notifications is only available over ``WebSocket`` connections.  

**!! Warning !! : DO NOT USE this feature when you are connecting mopidy server directly to the Internet.**  

This feature requires that Csrf-Protection be disabled.  
This setting is officially deprecated by mopidy.  

[Link: Mopidy-HTTP csrf_protection](https://docs.mopidy.com/en/latest/ext/http/#confval-http-csrf_protection):    
```
This config should only be disabled if you understand the security implications and require the HTTP server’s old behaviour.
```

How to get event notifications:  

1. Make sure that your mopidy server is NOT ACCESSIBLE from the Internet.
1. Set the ``http/csrf_protection`` value to ``false`` in your ``mopidy.conf``. 
1. Restart mopidy server.
1. Write Code Like:


```
// using Mopidy.Core;

Mopidy.Settings.ServerAddress = "Set your Mopidy IP-Address";
Mopidy.Settings.ConnectionType = Mopidy.Settings.Connection.WebSocket;

CoreListener.TrackPlaybackStarted += (sender, ev) =>
{
    System.Diagnostics.Debug.WriteLine(ev.TlTrack.Track.Name);
};

await Playback.Play();
```


#### More Info

[See Namespace.md](https://github.com/ume05rw/MopidySharp/blob/master/Namespace.md)


## Licence

[MIT Licence](https://github.com/ume05rw/MopidySharp/blob/master/LICENSE)

## Links

Mopidy Document Top:  
[https://docs.mopidy.com/en/latest/](https://docs.mopidy.com/en/latest/)  
  
Mopidy API:  
[https://docs.mopidy.com/en/latest/api/core/](https://docs.mopidy.com/en/latest/api/core/)  
