using System;

namespace InjectionMap.Configuration
{
    public interface IConfigurationExpression
    {
        /// <summary>
        /// Adds a propertydefinition that can be injected
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        IConfigurationExpression InjectProperty(Type type, string property);
    }
}
