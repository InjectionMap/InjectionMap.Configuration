using System;
using System.Configuration;

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
            foreach (var map in section.Mappings)
            {
                // extract the types defined in the config file
                var key = Type.GetType(map.Key);
                var reference = Type.GetType(map.For);
                
                // create a map that references itsself
                if(map.ToSelf)
                    reference = Type.GetType(map.Key);

                if (reference == null)
                {
                    throw new InjectionMap.Exceptions.MappingMismatchException(key, reference, string.Format("Type cannot be resolved from definition: {0}", string.IsNullOrEmpty(map.For) ? map.Key : map.For));
                }

                // Create a type object representing the generic ConfigurationComponentMapper type, by omitting the type arguments (but keeping the comma that separates them, 
                // so the compiler can infer the number of type parameters).      
                Type generic = typeof(ConfigurationComponentMapper<,>);

                // Create an array of types to substitute for the type parameters of Dictionary. The key is of type string, and the type to be contained in the Dictionary is Test.
                Type[] typeArgs = { key, reference };

                // Create a Type object representing the constructed generic type.
                Type constructed = generic.MakeGenericType(typeArgs);

                var componentMapper = Activator.CreateInstance(constructed) as IConfigurationComponentMapper;
                componentMapper.Map();
            }
        }
    }
}
