using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheCatAPIIntegration.ELKLog;
using TheCatDomain.Interfaces.Integration;

namespace TheCatAPIIntegration.LogExtensions
{
    /// <summary>
    /// Classe implementa extensão para ILogger
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Adiciona um Provider ILogger para integração com ELK
        /// </summary>
        /// <typeparam name="TIntegration"></typeparam>
        /// <param name="builder"></param>
        /// <param name="integration"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddELKLogProvider<TIntegration>(this ILoggingBuilder builder, TIntegration integration) where TIntegration : IELKIntegration
        {
            builder.Services.AddSingleton<ILoggerProvider, ELKLoggerProvider<TIntegration>>(p => new ELKLoggerProvider<TIntegration>((_, logLevel) => logLevel >= LogLevel.Debug, integration));
            return builder;
        }
    }
}
