using Mopidy.Models.JsonRpcs.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mopidy.Models.JsonRpcs
{
    internal class Query
    {
        internal static IQuery _instance = null;
        internal static IQuery Get()
        {
            if (Query._instance == null)
            {
                switch (Settings.ConnectionType)
                {
                    case Settings.Connection.HttpPost:
                        Query._instance = QueryHttp.Get();
                        break;
                    case Settings.Connection.WebSocket:
                        Query._instance = QueryWebSocket.Get();
                        break;
                    default:
                        throw new NotImplementedException($"Unexpected Connection Method: {Settings.ConnectionType}");
                }
            }

            return Query._instance;
        }
    }
}
