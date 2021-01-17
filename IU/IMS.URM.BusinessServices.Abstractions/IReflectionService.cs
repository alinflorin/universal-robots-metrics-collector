using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IReflectionService
    {
        PropertyInfo GetPropertyInfoByName<T>(string propertyName) where T : class;
    }
}
