using System;
using System.Collections.Generic;
using System.Linq;

namespace mis.Core
{
    public sealed class GameServices
    {
        private static GameServices _instance;
        private Dictionary<Type, IService> _gameServicesMap;

        private static readonly Type[] _typeBlacklist = new[]{
            typeof(IService),
            typeof(IStartableService),
        };

        public GameServices(IEnumerable<IService> services)
        {
            _gameServicesMap = new Dictionary<Type, IService>();

            foreach (var service in services)
            {
                var type = service.GetType();
                var interfaceTypes = type.GetInterfaces().Except(_typeBlacklist);

                if (interfaceTypes.Any())
                {
                    foreach (var interfaceType in interfaceTypes)
                    {
                        if (_gameServicesMap.ContainsKey(interfaceType))
                        {
                            throw new Exception($"Multiple isntances of {interfaceType.FullName}: {type.FullName}, {_gameServicesMap[interfaceType].GetType().FullName}");
                        }

                        _gameServicesMap[interfaceType] = service;
                    }
                }
            }

            _instance = this;
        }

        public static T Get<T>() where T : class, IService
        {
            if (!_instance._gameServicesMap.TryGetValue(typeof(T), out IService result))
            {
                throw new Exception($"No services of type {typeof(T).Name}");
            }

            return result as T;
        }

        public static T[] GetMultiple<T>() where T : class, IService
        {
            return _instance._gameServicesMap.Values
                .Where(service => service is T)
                .Select(service => service as T)
                .ToArray();
        }
    }
}
