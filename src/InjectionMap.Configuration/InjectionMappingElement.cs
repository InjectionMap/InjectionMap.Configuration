using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{

    public class InjectionMappingElement : ConfigurationElement
    {
        /// <summary>
        /// The key type that is used to mark the mapping
        /// </summary>
        [ConfigurationProperty("type", IsKey = true, IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }
    }
}
