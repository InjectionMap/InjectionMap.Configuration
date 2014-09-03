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

        [ConfigurationProperty("initializers", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(InjectionMappingElementCollection), AddItemName = "init")]
        public InjectionMappingElementCollection MapInitializers
        {
            get
            {
                return (InjectionMappingElementCollection)this["initializers"];
            }
        }
    }
}
