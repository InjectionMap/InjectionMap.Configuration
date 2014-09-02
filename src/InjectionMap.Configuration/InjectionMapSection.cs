using System.Configuration;

namespace InjectionMap.Configuration
{
    public class InjectionMapSection : ConfigurationSection
    {
        [ConfigurationProperty("mappings", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(MapElementCollection), AddItemName = "map")]
        public MapElementCollection Mappings
        {
            get
            {
                return (MapElementCollection)this["mappings"];
            }
        }
    }
}
