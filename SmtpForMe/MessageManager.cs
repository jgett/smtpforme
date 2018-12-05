using Microsoft.AspNet.SignalR;
using SmtpForMe.Controllers;
using SmtpForMe.Models;
using System.Collections.Concurrent;
using System.Linq;

namespace SmtpForMe
{
    public static class MessageManager
    {
        private static readonly ConcurrentDictionary<string, MessageModel> _messages = new ConcurrentDictionary<string, MessageModel>();

        private static IHubContext _hub => GlobalHost.ConnectionManager.GetHubContext<MessageHub>();

        public static int Count => _messages.Count;

        public static bool AddMessage(MessageModel model)
        {
            var result = _messages.TryAdd(model.Id, model);

            if (result)
                _hub.Clients.All.newMessage(model);

            return result;
        }

        public static MessageModel[] GetMessages()
        {
            return _messages.Select(x => x.Value).OrderByDescending(x => x.ReceivedOn).ToArray();
        }

        public static MessageModel GetMessage(string id)
        {
            return _messages[id];
        }

        public static bool DeleteAll()
        {
            if (Count > 0)
            { 
                _messages.Clear();
                return true;
            }

            return false;
        }

        public static bool DeleteMessage(string id)
        {
            return _messages.TryRemove(id, out MessageModel value);
        }
    }
}
