using InjectionMap.Tracing;
using System;

namespace InjectionMap.Configuration
{
    internal class ConfigurationMapper
    {
        internal Lazy<ILoggerFactory> LoggerFactory { get; private set; }

        internal ILogger Logger
        {
            get
            {
                return LoggerFactory.Value.CreateLogger();
            }
        }

        public ConfigurationMapper()
        {
            LoggerFactory = new Lazy<ILoggerFactory>(() => new LoggerFactory());
        }

        /// <summary>
        /// Registeres all mappings that were defined in the injectionMap section of application config file
        /// </summary>
        /// <param name="section"></param>
        /// <param name="context"></param>
        public void RegisterMappings(InjectionMapSection section, IMappingContext context)
        {
            foreach (var map in section.Mappings)
            {
                // extract the types defined in the config file
                var contract = Type.GetType(map.Contract);
                var reference = Type.GetType(map.MappedType);

                // create a map that references itsself
                if (map.ToSelf)
                    reference = Type.GetType(map.Contract);

                if (reference == null)
                {
                    Logger.Write(string.Format("InjectionMap.Configuration - Type cannot be resolved from definition: {0} because the Type cannot be identifie from {0}", string.IsNullOrEmpty(map.MappedType) ? map.Contract : map.MappedType), LogLevel.Error, "ConfigurationMapper", "Configuration");
                    throw new ResolverException(reference, string.Format("Type cannot be resolved from definition: {0} because the Type cannot be identifie from {0}", string.IsNullOrEmpty(map.MappedType) ? map.Contract : map.MappedType));
                }

                // Create a type object representing the generic ConfigurationComponentMapper type, by omitting the type arguments 
                Type generic = typeof(ConfigurationComponentMapper<,>);

                // Create a Type object representing the constructed generic type.
                Type constructed = generic.MakeGenericType(contract, reference);

                var componentMapper = Activator.CreateInstance(constructed, context) as IConfigurationComponentMapper;
                var expression = componentMapper.Map();

                foreach (var property in map.InjectionProperties)
                {
                    Logger.Write(string.Format("InjectionMap.Configuration - Create map for PropertyInjection on {0} for property {1}", contract.Name, property.Name), LogLevel.Info, "ConfigurationMapper", "Configuration");
                    expression.InjectProperty(contract, property.Name);
                }

                Logger.Write(string.Format("InjectionMap.Configuration - Mapped contract type {0} to {1}", contract.Name, reference.Name), LogLevel.Info, "ConfigurationMapper", "Configuration");
            }
        }

        /// <summary>
        /// Initializes all IMapiInitualizers that were defined in the injectionMap section of application config file
        /// </summary>
        /// <param name="section"></param>
        /// <param name="context"></param>
        public void RegisterInitializers(InjectionMapSection section, IMappingContext context)
        {
            foreach (var initDef in section.MapInitializers)
            {
                if (string.IsNullOrEmpty(initDef.Contract))
                    throw new ArgumentNullException("type");

                var type = Type.GetType(initDef.Contract);
                if (type == null)
                {
                    Logger.Write(string.Format("InjectionMap.COnfiguration - Cannot initialize IMapInitializer: {0} because the Type cannot be identifie from {0}", initDef.Contract), LogLevel.Error, "ConfigurationMapper", "Configuration");
                    throw new ResolverException(type, string.Format("Cannot initialize IMapInitializer: {0} because the Type cannot be identifie from {0}", initDef.Contract));
                }

                // create an instance of the MapInitializer
                var initializer = Activator.CreateInstance(type) as IMapInitializer;
                if (initializer == null)
                {
                    Logger.Write(string.Format("InjectionMap.COnfiguration - Instance could not be created of type {0}", type.Name), LogLevel.Warning, "ConfigurationMapper", "Configuration");
                    continue;
                }

                Logger.Write(string.Format("InjectionMap.COnfiguration - Initialized mappings from {0}", type.Name), LogLevel.Info, "ConfigurationMapper", "Configuration");

                if (context != null)
                {
                    // initialize the IMapInitializers
                    var init = new MapInitializer(context);
                    init.Initialize(initializer);
                }
                else
                {
                    var init = new MapInitializer();
                    init.Initialize(initializer);
                }
            }
        }
    }
}
