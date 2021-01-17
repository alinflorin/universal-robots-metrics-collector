using IMS.URM.BusinessServices.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IMS.URM.BusinessServices
{
    public class ReflectionService : IReflectionService
    {
        private readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _cache = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        public PropertyInfo GetPropertyInfoByName<T>(string propertyName) where T : class
        {
            var allProps = GetFromCacheOrBuild(typeof(T));
            PropertyInfo result;
            allProps.TryGetValue(propertyName, out result);
            return result;
        }

        private Dictionary<string, PropertyInfo> GetFromCacheOrBuild(Type type)
        {
            if (!_cache.ContainsKey(type))
            {
                _cache[type] = type.GetTypeInfo().DeclaredProperties.ToDictionary(x => x.Name, x => x);
            }
            return _cache[type];
        }
    }
}
