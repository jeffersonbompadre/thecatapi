using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Enumerables;

namespace TheCatDomain.Interfaces.Repositories
{
    public interface ILogEventRepository
    {
        Task<ICollection<LogEvent>> GetLogEvents(DateTime startDate, DateTime finishDate, EnumEventType eventType = EnumEventType.AllEvents);
        Task AddLogEvent(LogEvent logEvent);
    }
}
