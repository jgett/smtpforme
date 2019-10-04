using MimeKit;
using SmtpForMe.Models;
using SmtpServer;
using SmtpServer.Mail;
using SmtpServer.Protocol;
using SmtpServer.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                From = GetFromAddress(message),
                To = message.To.ToString(),
                Subject = message.Subject,
                HtmlBody = message.HtmlBody,
                TextBody = message.TextBody,
                Attachments = GetAttachments(message),
                ReceivedOn = receivedOn
            };

            MessageManager.AddMessage(model);

            return Task.FromResult(SmtpResponse.Ok);
        }

        private string GetFromAddress(MimeMessage message)
        {
            if (message.From.Count == 0)
                return "unknown";

            var from = (MailboxAddress)message.From.First();

            var result = string.Empty;

            if (!string.IsNullOrEmpty(from.Name))
                result = $"{from.Name} <{from.Address}>";
            else
                result = from.Address;

            return result;
        }

        private IEnumerable<AttachmentModel> GetAttachments(MimeMessage message)
        {
            var result = new List<AttachmentModel>();

            if (message.Attachments.Count(x => x.IsAttachment) == 0)
                return result;

            foreach (var att in message.Attachments)
            {
                var mp = (MimePart)att;

                result.Add(new AttachmentModel
                {
                    ContentType = mp.ContentType.MimeType,
                    FileName = mp.FileName,
                    Data = GetAttachmentData(mp)
                });
            }

            return result;
        }

        private byte[] GetAttachmentData(MimePart mp)
        {
            using (var ms = new MemoryStream())
            {
                mp.Content.DecodeTo(ms);
                ms.Position = 0;
                var result = ms.ToArray();
                ms.Close();
                return result;
            }
        }
    }
}