using AspNetCoreHero.Results;
using MailKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Security;
using Ryder.Application.Common.Hubs.Messaging;
using Ryder.Application.User.Command.EditUserProfile;
using Ryder.Domain.Context;
using Ryder.Domain.Entities;
using Ryder.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Command.UpdateMessage
{
    public class UpdateMessageCommandHanddler : IRequestHandler<UpdateMessageCommand, IResult>
    {
        private readonly ApplicationContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UpdateMessageCommandHanddler> _logger;
        private readonly IMessengerHub _messengerHub;

        public UpdateMessageCommandHanddler(ApplicationContext applicationContext, ICurrentUserService currentUserService, ILogger<UpdateMessageCommandHanddler> logger, IMessengerHub messengerHub)
        {
            _dbContext = applicationContext;
            _currentUserService = currentUserService;
            _logger = logger;
            _messengerHub = messengerHub;
        }
        
        
        
        
        public async Task<IResult> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var messageThread = await _dbContext.MessageThreads
                    .Include(x => x.Messages.Id.Equals(request.MessageId))
                    .FirstOrDefaultAsync(x => x.OrderId.Equals(request.OrderId));
                    

                if (messageThread == null)
                {
                    _logger.LogWarning($"MessageThread Not Found For Emojie Update");
                    return Result.Fail("Message Thread Not Found");
                }

                messageThread.Messages.Emojie = request.Emojie;
                messageThread.Messages.UpdatedAt = DateTime.UtcNow;

                var result = _dbContext.MessageThreads.Update(messageThread);

                int savedChanges = await _dbContext.SaveChangesAsync();

                if (savedChanges !> 0)
                {
                    _logger.LogCritical("Emojie Update Failed to Save");
                    return Result.Fail("Failed to send Message Update");
                }

                //send notification
                await _messengerHub.UpdateMessage(messageThread.Messages.ReceiverId, request.Emojie);

                _logger.LogInformation("Saved Message Update Successfully");
                return Result.Success("Update Successful");



                

            }
            catch (Exception)
            {
                _logger.LogError("Failed to Access Db On Message Update");

                throw;
            }

            throw new NotImplementedException();
        }
    }
}
