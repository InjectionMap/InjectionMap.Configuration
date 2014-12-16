using InjectionMap.Configuration.Internal;

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

        public IConfigurationExpression Map()
        {
            var expression = Map<TContract, TMap>() as IComponentExpression;
            
            return new ComponentExpression(Context as IComponentCollection, expression.Component);
        }
    }
}
