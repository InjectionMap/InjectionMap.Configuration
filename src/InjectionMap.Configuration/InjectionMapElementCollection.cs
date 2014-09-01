using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    public class InjectionMapElementCollection : ConfigurationElementCollection, IEnumerable<InjectionMapElement>
    {
        private readonly List<InjectionMapElement> _elements;

        public InjectionMapElementCollection()
        {
            _elements = new List<InjectionMapElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new InjectionMapElement();
            _elements.Add(element);

            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InjectionMapElement)element).Map;
        }

        public new IEnumerator<InjectionMapElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
