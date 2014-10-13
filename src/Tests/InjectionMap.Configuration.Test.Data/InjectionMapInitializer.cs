
namespace InjectionMap.Configuration.Test.Data
{
    public class InjectionMapInitializer : IMapInitializer
    {
        public void InitializeMap(IMappingProvider container)
        {
            container.Map<IContractThree, ObjectTypeThree>();
        }
    }
}
