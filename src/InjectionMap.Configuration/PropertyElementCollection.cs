using System.Collections.Generic;
using System.Configuration;

namespace InjectionMap.Configuration
{
    [ConfigurationCollection(typeof(MapElement))]
    public class PropertyElementCollection : ConfigurationElementCollection, IEnumerable<PropertyElement>
    {
        private readonly List<PropertyElement> _elements;

        public PropertyElementCollection()
        {
            _elements = new List<PropertyElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new PropertyElement();
            _elements.Add(element);

            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PropertyElement)element).Name;
        }

        public new IEnumerator<PropertyElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
