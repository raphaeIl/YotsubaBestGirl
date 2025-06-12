using Google.Protobuf;
using Serilog;
using System.Reflection;
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
        public IMessage? Invoke(Protocol protocol, IQueryCollection? reqParams);
        public MethodInfo? GetProtocolHandler(Protocol protocol);
        public void RegisterInstance(Type t, object? inst);
    }

    public class ProtocolHandlerFactory : IProtocolHandlerFactory
    {
        public static readonly Dictionary<string, Protocol> ProtocolIdToRouteMappings = new Dictionary<string, Protocol>()
        {
            ["AccountAuthorize"] = Protocol.AccountAuthorize,
            ["AccountCertificate"] = Protocol.AccountCertificate,
            ["UserLoad"] = Protocol.UserLoad,
            ["ResourceListAndroid"] = Protocol.ResourceListAndroid,
            ["ShopProducts"] = Protocol.ShopProducts,
            ["FcmToken"] = Protocol.FcmToken,
            ["MasterAll"] = Protocol.MasterAll,
            ["GachaPurchase"] = Protocol.GachaPurchase,
        };

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

        public IMessage? Invoke(Protocol msgId, IQueryCollection? reqParams)
        {
            var handler = GetProtocolHandler(msgId);
            if (handler is null)
                return null;

            handlerInstances.TryGetValue(handler.DeclaringType!, out var inst);
            return (IMessage?)handler.Invoke(inst, [reqParams]);
        }

        public MethodInfo? GetProtocolHandler(Protocol msgId)
        {
            handlers.TryGetValue(msgId, out var handler);

            return handler;
        }

        public static Protocol GetRequestProtocolByProtocolName(string msgId)
        {
            if (!ProtocolHandlerFactory.ProtocolIdToRouteMappings.ContainsKey(msgId))
            {
                return Protocol.Unknown;
            }

            Protocol protocolId = ProtocolHandlerFactory.ProtocolIdToRouteMappings[msgId];

            return protocolId;
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
