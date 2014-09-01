using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    public class InjectionMapSection : ConfigurationSection
    {
        [ConfigurationProperty("mappings", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(InjectionMapElementCollection), AddItemName = "add")]
        public InjectionMapElementCollection Mappings
        {
            get
            {
                return (InjectionMapElementCollection)this["mappings"];
            }
        }
    }
}
