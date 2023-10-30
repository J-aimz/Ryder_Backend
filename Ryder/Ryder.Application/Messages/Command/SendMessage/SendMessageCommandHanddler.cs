using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Ryder.Application.Common.Hubs.Messaging;
using Ryder.Domain.Context;
using Ryder.Domain.Entities;
using Ryder.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Command.SendMessage
{
    public class SendMessageCommandHanddler : IRequestHandler<SendMessageCommand, IResult>
    {
        //props
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<SendMessageCommandHanddler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessengerHub _messengerHub;

        //ctor
        public SendMessageCommandHanddler(ApplicationContext applicationContext, ILogger<SendMessageCommandHanddler> logger, ICurrentUserService currentUserService, IMessengerHub messengerHub)
        {
            _dbContext= applicationContext;
            _logger = logger;
            _currentUserService = currentUserService;
            _messengerHub = messengerHub;
        }



        public async Task<IResult> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var messageThread = await _dbContext.MessageThreads
                    .Include(x => x.Orders)
                    .Include(x => x.MessageThreadParticipants)
                    .Where(x => x.Orders.Id.Equals(request.OderId))
                    .FirstOrDefaultAsync(x => x.OrderId.Equals(request.OderId));
                    

                if (messageThread == null)
                {
                    _logger.LogError($"failed to get messageThread");
                    return Result.Fail("Opps Something Went Wrong");
                }

                Message message = new()
                {
                    MessageThreadId = messageThread.Id,
                    Body = request.Body,
                    //pls review later
                    SenderId = _currentUserService.UserId.Equals(messageThread.MessageThreadParticipants.AppUserId.ToString()) ? _currentUserService.UserId : messageThread.MessageThreadParticipants.RiderId.ToString(),
                    ReceiverId = !_currentUserService.UserId.Equals(messageThread.MessageThreadParticipants.AppUserId.ToString()) ? (messageThread.MessageThreadParticipants.RiderId.ToString()) : _currentUserService.UserId,

                };

                var postMessage = await _dbContext.Messages.AddAsync(message, cancellationToken);
                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    await _messengerHub.SendMessage(_currentUserService.UserId.Equals(messageThread.MessageThreadParticipants.AppUserId) ? messageThread.MessageThreadParticipants.RiderId.ToString() : messageThread.MessageThreadParticipants.AppUserId.ToString(), request.Body);

                    _logger.LogInformation("Meassge post Successfull");
                    return Result.Success("Success");
                }

                return Result.Fail("Failed");

            }
            catch (Exception ex)
            {
                _logger.LogError($"failed to access db when posting msg by user:{_currentUserService.UserId} with email:{_currentUserService.UserEmail}, trace: {ex.StackTrace}");
                return Result.Fail("Oops Something Went Wrong");
            }


            throw new NotImplementedException();
        }
    }
}
