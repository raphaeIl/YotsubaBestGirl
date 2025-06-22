using Google.Protobuf;
using Serilog;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;
using YotsubaBestGirl.Common.Core;
using YotsubaBestGirl.Core;
using YotsubaBestGirl.Proto.Proto;

namespace YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class ProtocolHandlerAttribute : Attribute
    {
        public Protocol Protocol { get; }

        public ProtocolHandlerAttribute(Protocol protocol)
        {
            Protocol = protocol;
        }
    }

    public interface IProtocolHandlerFactory
    {
        public HttpMessage? Invoke(Protocol protocol, RequestPacket req = null);
        public MethodInfo? GetProtocolHandler(Protocol protocol);
        public void RegisterInstance(Type t, object? inst);
    }

    public class ProtocolHandlerFactory : IProtocolHandlerFactory
    {
        private readonly Dictionary<Protocol, MethodInfo> handlers = [];
        private readonly Dictionary<Type, object?> handlerInstances = [];

        public ProtocolHandlerFactory()
        {

        }

        public void RegisterInstance(Type t, object? inst)
        {
            handlerInstances.Add(t, inst);

            foreach (var method in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(x => x.GetCustomAttribute<ProtocolHandlerAttribute>() is not null))
            {
                var attr = method.GetCustomAttribute<ProtocolHandlerAttribute>()!;
                if (handlers.ContainsKey(attr.Protocol))
                {
                    continue;
                }

                handlers.Add(attr.Protocol, method);
                Log.Information($"Loaded {method.Name} for {attr.Protocol}");
            }
        }

        public HttpMessage? Invoke(Protocol msgId, RequestPacket req)
        {
            var handler = GetProtocolHandler(msgId);
            if (handler is null)
                return null;

            handlerInstances.TryGetValue(handler.DeclaringType!, out var inst);

            // more features: optional req param
            var parameters = handler.GetParameters();
            object?[] args;

            // dynamically match parameter count
            if (parameters.Length == 0)
            {
                args = Array.Empty<object?>();
            } 
            
            else if (parameters.Length == 1)
            {
                args = new object?[] { req };
            } 
            
            else
            {
                throw new InvalidOperationException($"Handler for {msgId} has unsupported parameter count.");
            }

            // additional feature: handlers can either return a pure IMessage, or HttpMessage
            // pure IMessage: use default HttpMessage.Create, HttpMessage: just use it

            // return val is IMessage, meaning use default headers, with no gzip
            if (handler.ReturnType != typeof(HttpMessage))
            {
                IMessage packet = (IMessage?)handler.Invoke(inst, args);

                return HttpMessage.Create(packet);
            }

            // else, this means that inside the handler, we made a custom HttpMessage with custom headers, gzip, so just use that (this is currently quite rare with only like 3 packets needing this)
            return (HttpMessage?)handler.Invoke(inst, args);
        }

        public MethodInfo? GetProtocolHandler(Protocol msgId)
        {
            handlers.TryGetValue(msgId, out var handler);

            return handler;
        }
    }

    public abstract class ProtocolHandlerBase : IHostedService
    {
        private IProtocolHandlerFactory protocolHandlerFactory;

        public ProtocolHandlerBase(IProtocolHandlerFactory _protocolHandlerFactory)
        {
            protocolHandlerFactory = _protocolHandlerFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            protocolHandlerFactory.RegisterInstance(GetType(), this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    internal static class ServiceExtensions
    {
        public static void AddProtocolHandlerFactory(this IServiceCollection services)
        {
            services.AddSingleton<IProtocolHandlerFactory, ProtocolHandlerFactory>();
        }

        public static void AddProtocolHandlerGroup<T>(this IServiceCollection services) where T : ProtocolHandlerBase
        {
            services.AddHostedService<T>();
        }

        public static void AddProtocolHandlerGroupByType(this IServiceCollection services, Type type)
        {
            services.AddTransient(typeof(IHostedService), type);
        }
    }
}
