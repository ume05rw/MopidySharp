using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mopidy.Models.JsonRpcs.Interfaces
{
    internal interface IQuery
    {
        Task<JsonRpcParamsResponse> Exec(JsonRpcQuery request);
    }
}
