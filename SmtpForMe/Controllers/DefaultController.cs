using SmtpForMe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SmtpForMe.Controllers
{
    public class DefaultController : ApiController
    {
        [Route("api")]
        public string Get()
        {
            return "smtpforme-api";
        }

        [Route("api/messages")]
        public IEnumerable<MessageModel> GetMessages()
        {
            var messages = SmtpForMeMessageStore.Messages.Select(x => x.Value).ToArray();
            return messages.OrderByDescending(x => x.ReceivedOn);
        }

        [Route("api/message/{id}")]
        public bool DeleteMessage(string id)
        {
            var result = SmtpForMeMessageStore.Messages.TryRemove(id, out MessageModel value);
            return result;
        }

        [HttpGet, Route("")]
        public HttpResponseMessage Index()
        {
            var content = File.ReadAllText("index.html");

            var response = new HttpResponseMessage
            {
                Content = new StringContent(content)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        [HttpGet, Route("delete/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            DeleteMessage(id);
            var response = Request.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Location = new Uri(ConfigurationManager.AppSettings["WebServiceHost"]);
            return response;
        }
    }
}
