using AspNetCoreHero.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Query.GetTheadMessages
{
    public class GetThreadQuery : IRequest<IResult<MessageThreadDto>>
    {
        public string OrderId { get; set; }

        public GetThreadQuery(string orderId) => OrderId = orderId;
        
    }

    public class GetThreadCommandValidation : AbstractValidator<GetThreadQuery>
    {
        public GetThreadCommandValidation() => RuleFor(x => x.OrderId).NotEmpty().NotNull();  
    }
}
