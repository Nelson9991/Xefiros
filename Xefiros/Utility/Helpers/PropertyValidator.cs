using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xefiros.Utility.Helpers
{
    public static class PropertyValidator
    {
        public static bool IsValidProperty<T>(
            string propertyName,
            bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException(
                    $"ERROR: Property '{propertyName}' does not exist."
                );
            return prop != null;
        }
    }
}
