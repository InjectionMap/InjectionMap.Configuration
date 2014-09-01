using InjectionMap.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap
{
    public static class InjectionMapperExtension
    {
        public static void InitializeFromConfiguration(this InjectionMapper mapper)
        {
            var section = ConfigurationManager.GetSection("injectionMap") as InjectionMapSection;
            foreach (var map in section.Mappings)
            {
                //mapper.Map(
                throw new NotImplementedException();
            }
        }
    }
}
