using InjectionMap.Configuration;
using System;
using System.Configuration;

namespace InjectionMap
{
    public static class InjectionMapperExtension
    {
        /// <summary>
        /// Extension method that registers the mappings defined in the config file in the section named injectionMap
        /// </summary>
        /// <param name="mapper"></param>
        public static void Initialize(this MapInitializer initializer)
        {
            var section = ConfigurationManager.GetSection("injectionMap") as InjectionMapSection;
            var mapper = new ConfigurationMapper();

            mapper.RegisterMappings(section, initializer.Context);

            mapper.RegisterInitializers(section, initializer.Context);
        }
    }
}


