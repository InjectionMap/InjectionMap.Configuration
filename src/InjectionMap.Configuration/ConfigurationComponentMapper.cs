
namespace InjectionMap.Configuration
{
    public class ConfigurationComponentMapper<TContract, TMap> : ComponentMapper, IConfigurationComponentMapper where TMap : TContract
    {
        //public IBindingExpression<TMap> Map()
        //{
        //    return Map<TKey, TMap>();
        //}

        public void Map()
        {
            Map<TContract, TMap>();
        }
    }
}
