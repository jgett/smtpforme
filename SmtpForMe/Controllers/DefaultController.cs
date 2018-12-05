using Microsoft.AspNet.SignalR;
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
            var result = MessageManager.GetMessages();
            return result;
        }

        [Route("api/messages")]
        public bool DeleteMessages()
        {
            var result = MessageManager.DeleteAll();
            if (result) RefreshAll();
            return result;
        }

        [Route("api/message/{id}")]
        public MessageModel GetMessage(string id)
        {
            var result = MessageManager.GetMessage(id);
            return result;
        }

        [Route("api/message/{id}")]
        public bool DeleteMessage(string id)
        {
            var result = MessageManager.DeleteMessage(id);
            if (result) RefreshAll();
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

        private void RefreshAll()
        {
            IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hub.Clients.All.refresh(MessageManager.GetMessages());
        }
    }
}
