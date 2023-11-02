using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Ryder.Application.Messages.Query.GetTheadMessages;
using Ryder.Domain.Context;
using Ryder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Query.GetMessagesThead
{
    public class GetThreadQueryHandler : IRequestHandler<GetThreadQuery, IResult<MessageThreadDto>>
    {
        //props
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<GetThreadQueryHandler> _logger;


        //ctor
        public GetThreadQueryHandler(ApplicationContext applicationContext, ILogger<GetThreadQueryHandler> logger)
        {
            _dbContext = applicationContext;
            _logger = logger;
        }

        public async Task<IResult<MessageThreadDto>> Handle(GetThreadQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var messageThread = await _dbContext.MessageThreads
                    .Where(x => x.OrderId.Equals(request.OrderId))
                    .ToListAsync();

                if (messageThread == null)
                {
                    _logger.LogWarning($"No Messages Found on Thread For Order: {request.OrderId}");

                    return Result<MessageThreadDto>.Success(new MessageThreadDto());
                }

                List<Message> allMessages = new List<Message>();

                messageThread.ForEach(x =>
                {
                    allMessages.Add(x.Messages);
                });
                

                MessageThreadDto messages = new MessageThreadDto()
                {
                    MessageThread = messageThread.First().Id.ToString(),
                    OrderId = request.OrderId,
                    Messages = allMessages  
                };

                return Result<MessageThreadDto>.Success(messages);


                    
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message: {ex.Message}, Trace: {ex.StackTrace} ");
                return Result<MessageThreadDto>.Fail("Oops Something went Wrong");
            }

        }
    }
}
