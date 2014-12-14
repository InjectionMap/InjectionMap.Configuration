using System;

namespace InjectionMap.Configuration.Internal
{
    internal class ComponentExpression : IComponentExpression
    {
        public ComponentExpression(IComponentCollection context, IMappingComponent component)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (component == null)
                throw new ArgumentNullException("component");

            _context = context;
            _component = component;
        }

        readonly IComponentCollection _context;
        public IComponentCollection Context
        {
            get
            {
                return _context;
            }
        }

        readonly IMappingComponent _component;
        public IMappingComponent Component
        {
            get
            {
                return _component;
            }
        }
    }
}
