
namespace InjectionMap.Configuration
{
    public class ConfigurationComponentMapper<TContract, TMap> : ComponentMapper, IConfigurationComponentMapper where TMap : TContract
    {
        public ConfigurationComponentMapper()
        {
        }

        public ConfigurationComponentMapper(IMappingContext context)
            : base(context)
        {
        }
        
        public void Map()
        {
            Map<TContract, TMap>();
        }
    }
}
