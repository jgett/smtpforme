using Microsoft.AspNet.SignalR;
using SmtpForMe.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpForMe.Controllers
{
    public class MessageHub : Hub
    {
        public override Task OnConnected()
        {
            Refresh();
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            Refresh();
            return base.OnReconnected();
        }

        public void Refresh()
        {
            Clients.Caller.refresh(GetMessages());
        }

        public void DeleteMessage(string id)
        {
            SmtpForMeMessageStore.Messages.TryRemove(id, out MessageModel value);
            Clients.All.refresh(GetMessages());
        }

        public void DeleteAll()
        {
            SmtpForMeMessageStore.Messages.Clear();
            Clients.All.refresh(GetMessages());
        }

        private MessageModel[] GetMessages()
        {
            return SmtpForMeMessageStore.Messages.Select(x => x.Value).OrderByDescending(x => x.ReceivedOn).ToArray();
        }
    }
}
