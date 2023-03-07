using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using mis.Core;
using mis.Player;
using System.Collections.Generic;

public sealed class Bootstrap : IStartable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void OnApplicationStarting()
    {
        new Bootstrap().StartApplication();
    }

    private void StartApplication()
    {
        BuildDIContainer();
    }

    void IStartable.Start()
    {
        ExecuteStartupCommands();
    }

    private void BuildDIContainer()
    {
        var builder = new ContainerBuilder();
        RegisterDIContainer(builder);
        builder.Build();
    }

    private void RegisterDIContainer(IContainerBuilder builder)
    {
        RegisterGameServices(builder);
        RegisterMessageHandlers(builder);

        builder.RegisterInstance(this);
        builder.RegisterEntryPoint<Bootstrap>();
    }

    private void RegisterGameServices(IContainerBuilder builder)
    {
        var injectedServicesTypes = default(Type[]);
#if UNITY_EDITOR
        injectedServicesTypes = BoostrapSettings.Instance.ServicesTypes;
#else
        injectedServicesTypes = BootstrapServices.ServicesTypes;
#endif

        foreach (var serviceType in injectedServicesTypes)
        {
            builder.Register(serviceType, Lifetime.Singleton).AsImplementedInterfaces();
        }

        builder.Register<GameServices>(Lifetime.Singleton).AsSelf();
        builder.RegisterBuildCallback(container =>
        {
            container.Resolve<GameServices>();
        });
    }

    private void RegisterMessageHandlers(IContainerBuilder builder)
    {
        var injectedMessageHandlersTypes = default(Type[]);
#if UNITY_EDITOR
        injectedMessageHandlersTypes = BoostrapSettings.Instance.MessageHandlersTypes;
#else
        injectedMessageHandlersTypes = BoostrapMessageHandlers.MessageHandlers;
#endif

        foreach (var messageHandlerType in injectedMessageHandlersTypes)
        {
            builder.Register(messageHandlerType, Lifetime.Singleton).AsImplementedInterfaces();
        }

        builder.RegisterBuildCallback(container =>
        {
            container.Resolve<IReadOnlyList<IMessageHandler>>().ForEach(handler => handler.Subscribe());
        });
    }

    private void ExecuteStartupCommands()
    {
        var commands = new ICommand[]
        {
            new StartServicesCommand(),
            new CreatePlayerCommand(),
            new StartGameCommand(),
        };

        foreach (var cmd in commands)
        {
            cmd.Execute();
        }
    }
}