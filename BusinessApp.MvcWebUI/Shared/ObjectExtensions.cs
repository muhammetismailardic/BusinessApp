using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Shared
{
    public static class ObjectExtensions
    {
        // <summary>
        /// A nicer way of checking an object is null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// A nicer way of checking an object is not null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object value)
        {
            return IsNull(value) == false;
        }
    }
}
