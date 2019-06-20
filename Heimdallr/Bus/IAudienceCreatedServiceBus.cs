using ZenProgramming.Heimdallr.Bus.Messages;
using ZenProgramming.Chakra.Core.Bus;

namespace ZenProgramming.Heimdallr.Bus
{
    /// <summary>
    /// Interface for service bus on audience created message
    /// </summary>
    public interface IAudienceCreatedServiceBus: IServiceBus<AudienceCreatedMessage>
    {
    }
}
