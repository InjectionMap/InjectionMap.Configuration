using System.Configuration;

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
