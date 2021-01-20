using System;

namespace Mopidy.Models.JsonRpcs
{
    internal static class JsonRpcFactory
    {
        private static int _idForService = 90001;

        //public static JsonRpcQuery CreateQuery(JsonRpcParamsQuery values)
        //{
        //    var hasId = (values.Id != null);
        //    var hasParams = (values.Params != null);
        //    JsonRpcQuery query;

        //    if (hasId)
        //    {
        //        // id付き=リクエスト
        //        query = (hasParams)
        //            ? new JsonRpcQueryRequestWithParams((int)values.Id, values.Method, values.Params)
        //            : new JsonRpcQueryRequest((int)values.Id, values.Method);
        //    }
        //    else
        //    {
        //        // id無し=通知
        //        query = (hasParams)
        //            ? new JsonRpcQueryNoticeWithParams(values.Method, values.Params)
        //            : new JsonRpcQueryNotice(values.Method);
        //    }

        //    return query;
        //}

        public static JsonRpcQuery CreateRequest(string method, object @params = null)
        {
            JsonRpcQuery query;
            if (@params != null)
                query = new JsonRpcQueryRequestWithParams(JsonRpcFactory._idForService, method, @params);
            else
                query = new JsonRpcQueryRequest(JsonRpcFactory._idForService, method);

            JsonRpcFactory._idForService++;

            return query;
        }

        public static JsonRpcQuery CreateNotice(string method, object @params = null)
        {
            JsonRpcQuery query;
            if (@params != null)
                query = new JsonRpcQueryNoticeWithParams(method, @params);
            else
                query = new JsonRpcQueryNotice(method);

            return query;
        }

        //public static JsonRpcResult CreateResult(JsonRpcParamsResponse values)
        //{
        //    JsonRpcResult result;

        //    if (values.Error != null)
        //        result = new JsonRpcResultError((int)values.Id, values.Error);
        //    else
        //        result = new JsonRpcResultSucceeded((int)values.Id, values.Result);

        //    return result;
        //}

        public static JsonRpcResult CreateErrorResult(int id, Exception ex)
        {
            var result = new JsonRpcResultError(id, ex);

            return result;
        }
    }
}
