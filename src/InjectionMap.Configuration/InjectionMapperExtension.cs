using InjectionMap.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    public static class InjectionMapperExtension
    {
        public static void Initialize(this InjectionMapper mapper)
        {
            var section = ConfigurationManager.GetSection("injectionMap") as InjectionMapSection;
            foreach (var map in section.Mappings)
            {
                var key = Type.GetType(map.Key);
                var reference = Type.GetType(map.For);
                //mapper.Map(
                //throw new NotImplementedException();

                //http://msdn.microsoft.com/en-us/library/system.type.makegenerictype(v=vs.110).aspx


                // Create a type object representing the generic Dictionary  
                // type, by omitting the type arguments (but keeping the  
                // comma that separates them, so the compiler can infer the 
                // number of type parameters).      
                Type generic = typeof(ConfigurationComponentMapper<,>);
                DisplayTypeInfo(generic);

                // Create an array of types to substitute for the type 
                // parameters of Dictionary. The key is of type string, and 
                // the type to be contained in the Dictionary is Test.
                Type[] typeArgs = { key, reference };

                // Create a Type object representing the constructed generic 
                // type.
                Type constructed = generic.MakeGenericType(typeArgs);
                DisplayTypeInfo(constructed);

                var componentMapper = Activator.CreateInstance(constructed) as IConfigurationComponentMapper;
                componentMapper.Map();
            }
        }

        private static void DisplayTypeInfo(Type t)
        {
            Console.WriteLine("\r\n{0}", t);

            Console.WriteLine("\tIs this a generic type definition? {0}",
                t.IsGenericTypeDefinition);

            Console.WriteLine("\tIs it a generic type? {0}",
                t.IsGenericType);

            Type[] typeArguments = t.GetGenericArguments();
            Console.WriteLine("\tList type arguments ({0}):", typeArguments.Length);
            foreach (Type tParam in typeArguments)
            {
                Console.WriteLine("\t\t{0}", tParam);
            }
        }
    }
}
