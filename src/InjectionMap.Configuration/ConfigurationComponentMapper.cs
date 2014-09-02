using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InjectionMap.Expressions;

namespace InjectionMap.Configuration
{
    public class ConfigurationComponentMapper<TKey, TMap> : ComponentMapper, IConfigurationComponentMapper where TMap : TKey
    {
        //public IBindingExpression<TMap> Map()
        //{
        //    return Map<TKey, TMap>();
        //}

        public void Map()
        {
            Map<TKey, TMap>();
        }
    }
}
