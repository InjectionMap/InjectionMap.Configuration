using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    [ConfigurationCollection(typeof(InjectionMappingElement))]
    public class InjectionMappingElementCollection : ConfigurationElementCollection, IEnumerable<InjectionMappingElement>
    {
        private readonly List<InjectionMappingElement> _elements;

        public InjectionMappingElementCollection()
        {
            _elements = new List<InjectionMappingElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new InjectionMappingElement();
            _elements.Add(element);

            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InjectionMappingElement)element).Contract;
        }

        public new IEnumerator<InjectionMappingElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
