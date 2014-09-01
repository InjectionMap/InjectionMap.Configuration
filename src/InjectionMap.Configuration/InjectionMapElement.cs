using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    public class InjectionMapElement : ConfigurationElement
    {
        [ConfigurationProperty("map", IsKey = true, IsRequired = true)]
        public string Map
        {
            get
            {
                return (string)this["map"];
            }
            set
            {
                this["map"] = value;
            }
        }

        [ConfigurationProperty("for", IsKey = true, IsRequired = false)]
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

        [ConfigurationProperty("toSelf", IsKey = true, IsRequired = false)]
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
