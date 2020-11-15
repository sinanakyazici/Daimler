using Serilog.Core;
using Serilog.Events;

namespace Daimler.Lib.Logger
{
    public class DefaultContextEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {

        }
    }
}