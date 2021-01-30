using System;

namespace Mopidy.Models.JsonRpcs
{
    internal static class JsonRpcFactory
    {
        private class Locker
        {
            public bool IsLocked { get; private set; } = false;

            public TResult LockedInvoke<TResult>(Func<TResult> func)
            {
                TResult result = default(TResult);
                lock (this)
                {
                    this.IsLocked = true;

                    try
                    {
                        result = func.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }

                    this.IsLocked = false;
                }

                return result;
            }
        }

        private static int _idForService = 90000;
        private static Locker _locker = new Locker();

        public static JsonRpcQuery CreateRequest(string method, object @params = null)
        {
            return JsonRpcFactory._locker.LockedInvoke(() =>
            {
                JsonRpcFactory._idForService++;

                return (@params != null)
                    ? new JsonRpcQueryRequestWithParams(JsonRpcFactory._idForService, method, @params)
                    : new JsonRpcQueryRequest(JsonRpcFactory._idForService, method);
            });
        }

        public static JsonRpcQuery CreateNotice(string method, object @params = null)
        {
            return (@params != null)
                ? new JsonRpcQueryNoticeWithParams(method, @params)
                : new JsonRpcQueryNotice(method);
        }


        // Not Used.
        //public static JsonRpcQuery CreateQuery(JsonRpcParamsQuery values)
        //{
        //    var hasId = (values.Id != null);
        //    var hasParams = (values.Params != null);
        //    JsonRpcQuery query;
        //
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
        //
        //    return query;
        //}
        //
        //public static JsonRpcResult CreateResult(JsonRpcParamsResponse values)
        //{
        //    JsonRpcResult result;
        //
        //    if (values.Error != null)
        //        result = new JsonRpcResultError((int)values.Id, values.Error);
        //    else
        //        result = new JsonRpcResultSucceeded((int)values.Id, values.Result);
        //
        //    return result;
        //}
        //
        //public static JsonRpcResult CreateErrorResult(int id, Exception ex)
        //{
        //    var result = new JsonRpcResultError(id, ex);
        //
        //    return result;
        //}
    }
}
