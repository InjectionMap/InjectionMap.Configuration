using System;
using System.Configuration;
using System.Diagnostics;

namespace InjectionMap.Configuration
{
    public static class InjectionMapperExtension
    {
        /// <summary>
        /// Extension method that registers the mappings defined in the config file in the section named injectionMap
        /// </summary>
        /// <param name="mapper"></param>
        public static void Initialize(this InjectionMapper mapper)
        {
            var section = ConfigurationManager.GetSection("injectionMap") as InjectionMapSection;

            RegisterMappings(section);

            RegisterInitializers(section);
        }

        /// <summary>
        /// Registeres all mappings that were defined in the injectionMap section of application config file
        /// </summary>
        /// <param name="section"></param>
        private static void RegisterMappings(InjectionMapSection section)
        {
            foreach (var map in section.Mappings)
            {
                // extract the types defined in the config file
                var key = Type.GetType(map.Key);
                var reference = Type.GetType(map.For);

                // create a map that references itsself
                if (map.ToSelf)
                    reference = Type.GetType(map.Key);

                if (reference == null)
                {
                    throw new ResolverException(reference, string.Format("Type cannot be resolved from definition: {0} because the Type cannot be identifie from {0}", string.IsNullOrEmpty(map.For) ? map.Key : map.For));
                }

                // Create a type object representing the generic ConfigurationComponentMapper type, by omitting the type arguments 
                Type generic = typeof(ConfigurationComponentMapper<,>);

                // Create an array of types to substitute for the type parameters of Dictionary. The key is of type string, and the type to be contained in the Dictionary is Test.
                Type[] typeArgs = { key, reference };

                // Create a Type object representing the constructed generic type.
                Type constructed = generic.MakeGenericType(typeArgs);

                var componentMapper = Activator.CreateInstance(constructed) as IConfigurationComponentMapper;
                componentMapper.Map();
            }
        }

        /// <summary>
        /// Initializes all IMapiInitualizers that were defined in the injectionMap section of application config file
        /// </summary>
        /// <param name="section"></param>
        private static void RegisterInitializers(InjectionMapSection section)
        {
            foreach (var initDef in section.MapInitializers)
            {
                if (string.IsNullOrEmpty(initDef.Type))
                    throw new ArgumentNullException("type");

                var type = Type.GetType(initDef.Type);
                if (type == null)
                {
                    throw new ResolverException(type, string.Format("Cannot initialize IMapInitializer: {0} because the Type cannot be identifie from {0}", initDef.Type));
                }

                // create an instance of the MapInitializer
                var initializer = Activator.CreateInstance(type) as IInjectionMapping;
                if (initializer == null)
                {
                    Trace.WriteLine(string.Format("Instance could not be created of type {0}", type.ToString()));
                    continue;
                }

                // initialize the IMapInitializers
                InjectionMapper.Initialize(initializer);
            }
        }
    }
}


