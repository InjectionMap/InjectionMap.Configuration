
namespace InjectionMap.Configuration.Test.Data
{
    public class InjectionMapInitializer : IInjectionMapping
    {
        public void InitializeMap(IMappingProvider container)
        {
            container.Map<IKeyThree, ObjectTypeThree>();
        }
    }
}
