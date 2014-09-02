using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionMap.Configuration
{
    [ConfigurationCollection(typeof(MapElement))]
    public class MapElementCollection : ConfigurationElementCollection, IEnumerable<MapElement>
    {
        private readonly List<MapElement> _elements;

        public MapElementCollection()
        {
            _elements = new List<MapElement>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new MapElement();
            _elements.Add(element);

            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MapElement)element).Key;
        }

        public new IEnumerator<MapElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
