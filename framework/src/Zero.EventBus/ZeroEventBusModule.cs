using Zero.Core.Modularity;
using Zero.EventBus.Abstractions;

namespace Zero.EventBus
{
    [DependsOn(typeof(ZeroEventBusAbstractionsModule))]
    public class ZeroEventBusModule : ZeroModule
    {
    }
}
