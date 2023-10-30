using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Command.SendMessage
{
    public class MessageDto
    {
        public string OderId { get; set; }
        //public string SenderId { get; set; } 
        //public string ReceiverId { get; set; }
        public string Body { get; set; }
    }
}
