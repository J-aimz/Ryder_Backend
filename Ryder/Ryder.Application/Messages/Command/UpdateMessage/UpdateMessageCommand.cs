using AspNetCoreHero.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Command.UpdateMessage
{
    public class UpdateMessageCommand : IRequest<IResult>
    {
        public string OrderId { get; set; }
        public string MessageId { get; set; }
        public string Emojie { get; set; }

        public UpdateMessageCommand(EmojieDto emojieDto)
        {
            OrderId = emojieDto.OrderId;
            Emojie = emojieDto.Emojie; 
            MessageId = emojieDto.MessageId;    
        }
    } 

    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().NotNull();
            RuleFor(x => x.MessageId).NotEmpty().NotNull();
            RuleFor(x => x.Emojie).NotEmpty().NotNull();
        }
    }
}
