﻿using System;
using System.Collections.Generic;

namespace SmtpForMe.Models
{
    public class MessageModel
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public IEnumerable<AttachmentModel> Attachments { get; set; }
        public DateTime ReceivedOn { get; set; }
    }
}
