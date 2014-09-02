using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    public class MapElement : ConfigurationElement
    {
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

        [ConfigurationProperty("toSelf", IsRequired = false)]
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
