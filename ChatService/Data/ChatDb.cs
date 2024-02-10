using ChatService.Models;
using System.Collections.Concurrent;

namespace ChatService.Data
{
    public class ChatDb //In-Memory Db
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connection;
        public ConcurrentDictionary<string, UserConnection> connections => _connection;
    }
}
