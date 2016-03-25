using InjectionMap.Tracing;
using System;

namespace InjectionMap.Configuration
{
    internal class ConfigurationMapper
    {
        private const string SOURCE = "ConfigurationMapper";
        private const string CATEGORY = "Configuration";

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
                {
                    reference = Type.GetType(map.Contract);
                }

                if (reference == null)
                {
                    var message = string.Format(format: "Type cannot be resolved from definition: {0}\nThe Type cannot be identified from {0}", arg0: string.IsNullOrEmpty(map.MappedType) ? map.Contract : map.MappedType);
                    Logger.Write(string.Concat(str0: "InjectionMap.Configuration - ", str1: message), LogLevel.Error, SOURCE, CATEGORY);
                    throw new ResolverException(reference, message);
                }

                // Create a type object representing the generic ConfigurationComponentMapper type, by omitting the type arguments 
                Type generic = typeof(ConfigurationComponentMapper<,>);

                // Create a Type object representing the constructed generic type.
                Type constructed = generic.MakeGenericType(contract, reference);

                var componentMapper = Activator.CreateInstance(constructed, context) as IConfigurationComponentMapper;
                var expression = componentMapper.Map();

                foreach (var property in map.InjectionProperties)
                {
                    Logger.Write(string.Format(format: "InjectionMap.Configuration - Create map for PropertyInjection on {0} for property {1}", arg0: contract.Name, arg1: property.Name), LogLevel.Info, source: SOURCE, category: CATEGORY);
                    expression.InjectProperty(contract, property.Name);
                }

                Logger.Write(string.Format(format: "InjectionMap.Configuration - Mapped contract type {0} to {1}", arg0: contract.Name, arg1: reference.Name), LogLevel.Info, source: SOURCE, category: CATEGORY);
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
                {
                    throw new ArgumentNullException("type");
                }

                var type = Type.GetType(initDef.Contract);
                if (type == null)
                {
                    var message = string.Format(format: "Cannot initialize IMapInitializer: {0}. The Type cannot be identified or does not exist.", arg0: initDef.Contract);
                    Logger.Write(string.Concat(str0: "InjectionMap.COnfiguration - ", str1: message), LogLevel.Error, source: SOURCE, category: CATEGORY);
                    throw new ResolverException(type, message);
                }

                // create an instance of the MapInitializer
                var initializer = Activator.CreateInstance(type) as IMapInitializer;
                if (initializer == null)
                {
                    Logger.Write(string.Format(format: "InjectionMap.COnfiguration - Instance could not be created of type {0}", arg0: type.Name), LogLevel.Warning, source: SOURCE, category: CATEGORY);
                    continue;
                }

                Logger.Write(string.Format(format: "InjectionMap.COnfiguration - Initialized mappings from {0}", arg0: type.Name), LogLevel.Info, source: SOURCE, category: CATEGORY);

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
