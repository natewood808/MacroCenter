using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Infrastructure
{
    /// <summary>
    /// Use this class to serialize our Cart objects to store them in session state
    /// since ASP.NET Core's session state feature only holds int, string, or byte[]
    /// data types. An interesting observation was JsonConvert class is made available
    /// through the Newtonsoft.Json package we implemented in task 38.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// This extension method is used to add Cart objects to the session state.
        /// This can be done by calling HttpContext.Session.SetJson("Cart", cart); in the CartController.cs.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// This extension method acts on types that implement ISession. This is used to deserialize
        /// objects by passing in the key that was used to serialize the object in the SetJson() call.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
