using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fambook.MessageService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fambook.MessageService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var id = 0;

            var messages = new List<Message>();
            for (int i = 0; i < 6; i++)
            {
                messages.Add(new Message
                {
                    Id = id++,
                    Text = "This is a message object with id: " + id
                });
            }

            return messages;
        }
    }
}