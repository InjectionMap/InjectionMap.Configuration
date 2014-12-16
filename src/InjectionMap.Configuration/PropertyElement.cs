using System.Configuration;

namespace InjectionMap.Configuration
{
    public class PropertyElement : ConfigurationElement
    {
        /// <summary>
        /// The name of the property
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        ///// <summary>
        ///// The type that will be mapped and resolved
        ///// </summary>
        //[ConfigurationProperty("mappedType", IsRequired = false)]
        //public string MappedType
        //{
        //    get
        //    {
        //        return (string)this["mappedType"];
        //    }
        //    set
        //    {
        //        this["mappedType"] = value;
        //    }
        //}

        ///// <summary>
        ///// Defines if the contract type is mapped to itsself an has to be resolved
        ///// </summary>
        //[ConfigurationProperty("toSelf", DefaultValue = false, IsRequired = false)]
        //public bool ToSelf
        //{
        //    get
        //    {
        //        return (bool)this["toSelf"];
        //    }
        //    set
        //    {
        //        this["toSelf"] = value;
        //    }
        //}
    }
}
