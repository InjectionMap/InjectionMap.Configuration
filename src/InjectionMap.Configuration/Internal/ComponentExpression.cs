using System;
using InjectionMap.Tracing;
using InjectionMap.Composition;

namespace InjectionMap.Configuration.Internal
{
    internal class ComponentExpression : IComponentExpression, IConfigurationExpression
    {
        internal Lazy<ILoggerFactory> LoggerFactory { get; private set; }

        internal ILogger Logger
        {
            get
            {
                return LoggerFactory.Value.CreateLogger();
            }
        }

        public ComponentExpression(IComponentCollection context, IMappingComponent component)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (component == null)
                throw new ArgumentNullException("component");

            _context = context;
            _component = component;

            LoggerFactory = new Lazy<ILoggerFactory>(() => new LoggerFactory());
        }

        #region IComponentExpression

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

        #endregion

        #region IConfigurationExpression

        public IConfigurationExpression InjectProperty(Type type, string property)
        {
            if (string.IsNullOrEmpty(property))
                throw new ArgumentNullException("property");

            var prop = type.GetProperty(property);

            if (prop == null)
            {
                Logger.Write(string.Format("InjectionMap.Configuration - Could not find property {0} on type {1} for propertyinjection", property, type.Name), LogLevel.Warning, "ConfigurationExpression", "Configuration");
                return this;
            }

            var factory = new TypeDefinitionFactory();
            var setter = factory.GetPropertySetter(prop);
            Component.Properies.Add(new PropertyDefinition
            {
                KeyType = prop.PropertyType,
                Property = prop,
                Setter = setter
            });

            return this;
        }

        #endregion
    }
}
