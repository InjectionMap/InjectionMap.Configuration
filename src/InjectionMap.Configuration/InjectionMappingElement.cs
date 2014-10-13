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
        /// The contract type that is used to mark the mapping
        /// </summary>
        [ConfigurationProperty("contract", IsKey = true, IsRequired = true)]
        public string Contract
        {
            get
            {
                return (string)this["contract"];
            }
            set
            {
                this["contract"] = value;
            }
        }
    }
}
