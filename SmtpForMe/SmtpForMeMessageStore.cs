using MimeKit;
using SmtpForMe.Models;
using SmtpServer;
using SmtpServer.Mail;
using SmtpServer.Protocol;
using SmtpServer.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmtpForMe
{
    public class SmtpForMeMessageStore : MessageStore
    {
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

            MessageManager.AddMessage(model);

            return Task.FromResult(SmtpResponse.Ok);
        }
    }
}