using ChatService.Data;
using ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatDb _chatDb;
        public ChatHub(ChatDb chatDb)
        {
            _chatDb = chatDb;
        }
        public async Task SendMessageAsync(string message)
        {
            if (_chatDb.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveCertainMessage", conn.UserName, message);
            }
        }
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage","admin",$"{conn.UserName} has joined");
        }
        public async Task JoinCertainChat(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            _chatDb.connections[Context.ConnectionId] = conn;
            await Clients.Groups(conn.ChatRoom).SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined {conn.ChatRoom}");
        }
    }
}
