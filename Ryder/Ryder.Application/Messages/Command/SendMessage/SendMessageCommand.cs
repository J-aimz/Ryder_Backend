using AspNetCoreHero.Results;
using FluentValidation;
using MediatR;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Command.SendMessage
{
    public class SendMessageCommand : IRequest<IResult>
    {
        //props
        public string OderId { get; set; }
        //public string SenderId { get; set; } 
        //public string ReceiverId { get; set; }
        public string Body { get; set; }

        public SendMessageCommand(string oderId, string senderId, string receiverId, string body)
        {
            OderId = oderId;
            //SenderId= senderId;
            //ReceiverId= receiverId;
            Body = body;
        }
    }



    public class SendMessageCommandValidation : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidation()
        {
            RuleFor(x => x.OderId).NotEmpty().NotNull();
            //RuleFor(x => x.SenderId).NotNull().NotEmpty();
            //RuleFor(x => x.ReceiverId).NotNull().NotEmpty();
            RuleFor(x => x.Body).NotNull().NotEmpty();
        }
    }
}
