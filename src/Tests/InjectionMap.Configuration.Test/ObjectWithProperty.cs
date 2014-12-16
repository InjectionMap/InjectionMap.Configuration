using InjectionMap.Configuration.Test.Data;

namespace InjectionMap.Configuration.Test
{
    public interface IObjectWithProperty
    {
        IContractOne Contract { get; set; }

        IContractOne ContractOne { get; set; }
    }

    public class ObjectWithProperty : IObjectWithProperty
    {

        public IContractOne Contract { get; set; }

        public IContractOne ContractOne { get; set; }
    }
}
