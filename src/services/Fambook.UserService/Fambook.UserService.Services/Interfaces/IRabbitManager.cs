using System;
using System.Collections.Generic;
using System.Text;

namespace Fambook.UserService.Services.Interfaces
{
    public interface IRabbitManager
    {
        void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class;
    }
}
