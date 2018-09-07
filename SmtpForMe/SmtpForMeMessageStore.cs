using Microsoft.AspNet.SignalR;
using MimeKit;
using SmtpForMe.Controllers;
using SmtpForMe.Models;
using SmtpServer;
using SmtpServer.Mail;
using SmtpServer.Protocol;
using SmtpServer.Storage;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SmtpForMe
{
    public class SmtpForMeMessageStore : MessageStore
    {
        public static ConcurrentDictionary<string, MessageModel> Messages { get; }

        static SmtpForMeMessageStore()
        {
            Messages = new ConcurrentDictionary<string, MessageModel>();
        }

        public override Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString("n");
            var receivedOn = DateTime.Now;

            var textMessage = (ITextMessage)transaction.Message;

            var message = MimeMessage.Load(textMessage.Content);

            var model = new MessageModel()
            {
                Id = id,
                From = message.From.ToString(),
                To = message.To.ToString(),
                Subject = message.Subject,
                HtmlBody = message.HtmlBody,
                TextBody = message.TextBody,
                ReceivedOn = receivedOn
            };

            Messages.TryAdd(id, model);

            var signalr = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            signalr.Clients.All.newMessage(model);

            return Task.FromResult(SmtpResponse.Ok);
        }
    }
}