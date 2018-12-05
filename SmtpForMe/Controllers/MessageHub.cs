using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SmtpForMe.Controllers
{
    public class MessageHub : Hub
    {
        public override Task OnConnected()
        {
            RefreshCaller();
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            RefreshCaller();
            return base.OnReconnected();
        }

        public void DeleteAll()
        {
            if (MessageManager.DeleteAll())
                RefreshAll();
        }

        public void DeleteMessage(string id)
        {
            if (MessageManager.DeleteMessage(id))
                RefreshAll();
        }

        private void RefreshAll()
        {
            Clients.All.refresh(MessageManager.GetMessages());
        }

        private void RefreshCaller()
        {
            Clients.Caller.refresh(MessageManager.GetMessages());
        }
    }
}
