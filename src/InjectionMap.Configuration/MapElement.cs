using System.Configuration;

namespace InjectionMap.Configuration
{
    public class MapElement : ConfigurationElement
    {
        /// <summary>
        /// The key type that is used to mark the mapping
        /// </summary>
        [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        /// <summary>
        /// The type that will be mapped and resolved
        /// </summary>
        [ConfigurationProperty("for", IsRequired = false)]
        public string For
        {
            get
            {
                return (string)this["for"];
            }
            set
            {
                this["for"] = value;
            }
        }

        /// <summary>
        /// Defines if the key type is mapped to itsself an has to be resolved
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
    }
}
