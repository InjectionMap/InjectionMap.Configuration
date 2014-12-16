using System.Configuration;

namespace InjectionMap.Configuration
{
    public class MapElement : ConfigurationElement
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

        /// <summary>
        /// The type that will be mapped and resolved
        /// </summary>
        [ConfigurationProperty("mappedType", IsRequired = false)]
        public string MappedType
        {
            get
            {
                return (string)this["mappedType"];
            }
            set
            {
                this["mappedType"] = value;
            }
        }

        /// <summary>
        /// Defines if the contract type is mapped to itsself an has to be resolved
        /// </summary>
        [ConfigurationProperty("toSelf", DefaultValue = false, IsRequired = false)]
        public bool ToSelf
        {
            get
            {
                return (bool)this["toSelf"];
            }
            set
            {
                this["toSelf"] = value;
            }
        }


        [ConfigurationProperty("properties", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(PropertyElementCollection), AddItemName = "property")]
        public PropertyElementCollection InjectionProperties
        {
            get
            {
                return (PropertyElementCollection)this["properties"];
            }
        }
    }
}
