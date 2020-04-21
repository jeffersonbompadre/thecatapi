using Microsoft.Extensions.Logging;
using System;
using TheCatDomain.Interfaces.Integration;

namespace TheCatAPIIntegration.ELKLog
{
    /// <summary>
    /// Classe cria um Provider para integração com o ELK
    /// </summary>
    /// <typeparam name="TIntegration"></typeparam>
    public class ELKLoggerProvider<TIntegration> : ILoggerProvider where TIntegration : IELKIntegration
    {
        readonly Func<string, LogLevel, bool> _filter;
        readonly TIntegration _integration;

        public ELKLoggerProvider(Func<string, LogLevel, bool> filter, TIntegration integration)
        {
            _filter = filter;
            _integration = integration;
        }

        public ILogger CreateLogger(string categoryName) => new ELKLogger<TIntegration>(_filter, _integration, categoryName);

        public void Dispose()
        {
        }
    }
}
