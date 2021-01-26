MopidySharp - Library for Mopidy Music Server
====

Implemented all methods on mopidy.core.  
Connect by Http-Post/JSON-RPC. (Not WebSocket)   

## Description

Supports .NET Standard2.0

## Requirement

[Newtonsoft.Json ver >= 12.0.3](https://www.nuget.org/packages/Newtonsoft.Json/)  
[System.Drawing.Common ver >= 5.0.0](https://www.nuget.org/packages/System.Drawing.Common/)  

## Usage

1: [Add NuGet-Package](https://www.nuget.org/packages/MopidySharp/) to your project.  
2: Write Code Like:  

    // using Mopidy.Core;
    
    Mopidy.Settings.ServerAddress = "Set your Mopidy IP-Address";  
    var res = await Tracklist.GetTlTracks();  
    
    if (res.Succeeded)
        foreach (var tlTrack in res.Result)
            System.Diagnostics.Debug.WriteLine(tlTrack.Track.Name);

More info: [Namespace.md](https://github.com/ume05rw/MopidySharp/blob/master/Namespace.md])


## Licence

[MIT Licence](https://github.com/ume05rw/MopidySharp/blob/master/LICENSE)

## Links

Mopidy Document Top:  
[https://docs.mopidy.com/en/latest/](https://docs.mopidy.com/en/latest/)  
  
Mopidy API:  
[https://docs.mopidy.com/en/latest/api/core/](https://docs.mopidy.com/en/latest/api/core/)  
