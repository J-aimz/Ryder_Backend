using AspNetCoreHero.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ryder.Application.Common.Hubs;
using Ryder.Domain.Context;
using Ryder.Domain.Entities;
using Ryder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Reflection.Metadata.Ecma335;
using Ryder.Infrastructure.Interface;

namespace Ryder.Application.Order.Command.PlaceOrder
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, IResult<Guid>>
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationHub _notificationHub;
        private readonly ICurrentUserService _currentUser;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public PlaceOrderCommandHandler(ApplicationContext context, UserManager<AppUser> userManager,
            INotificationHub notificationHub, ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _notificationHub = notificationHub;
            _currentUser = currentUserService;
        }

        public async Task<IResult<Guid>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var currentUser = await _userManager.FindByIdAsync(request.AppUserId.ToString());

                if (currentUser == null)
                {
                    return Result<Guid>.Fail("User not found");
                }

                var order = new Domain.Entities.Order
                {
                    Id = Guid.NewGuid(),
                    PickUpLocation = new Address
                    {
                        City = request.PickUpLocation.City,
                        State = request.PickUpLocation.State,
                        PostCode = request.PickUpLocation.PostCode,
                        Longitude = request.PickUpLocation.Longitude,
                        Latitude = request.PickUpLocation.Latitude,
                        Country = request.PickUpLocation.Country,
                        AddressDescription = request.PickUpLocation.AddressDescription,
                    },
                    DropOffLocation = new Address
                    {
                        City = request.PickUpLocation.City,
                        State = request.PickUpLocation.State,
                        PostCode = request.PickUpLocation.PostCode,
                        Longitude = request.PickUpLocation.Longitude,
                        Latitude = request.PickUpLocation.Latitude,
                        Country = request.PickUpLocation.Country,
                        AddressDescription = request.DropOffLocation.AddressDescription,
                    },
                    PickUpPhoneNumber = request.PickUpPhoneNumber,
                    PackageDescription = request.PackageDescription,
                    ReferenceNumber = request.ReferenceNumber,
                    Amount = request.Amount,
                    RiderId = Guid.Empty,
                    AppUserId = currentUser.Id,
                    Status = OrderStatus.OrderPlaced,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Email = currentUser.Email,
                    Name = currentUser.FirstName +" "+ currentUser.LastName
                };

                //send notification to riders
                Task.Run( () => RunNotification(_cancellationTokenSource.Token));



                await _context.Orders.AddAsync(order, cancellationToken);

                //Add customer to message thread
                MessageThread messageThread = new MessageThread
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id.ToString()
                };

                MessageThreadParticipant participant = new MessageThreadParticipant
                {
                    Id = Guid.NewGuid(),
                    MessageThreadId = messageThread.Id,
                    AppUserId = order.Id
                };


                //Add user to Message thread
                await _context.MessageThreads.AddAsync(messageThread);
                await _context.MessageThreadParticipants.AddAsync(participant);  



                await _context.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(order.Id, "Order placed successfully");
            }
            catch (Exception)
            {
                return Result<Guid>.Fail("Order not placed");
            }
        }


        public async Task RunNotification(CancellationToken cancellationToken)
        {
            List<string> getAllAvailableRiders = new();

            bool verify = true;
            int attempts = 0;
            int maxAttempts = 10;

            do
            {
                try
                {
                    getAllAvailableRiders = await _context.Riders
                        .Where(x => x.AvailabilityStatus == RiderAvailabilityStatus.Available)
                        .Include(x => x.AppUser)
                        .Select(x => x.AppUser.UserName.ToString()).ToListAsync(cancellationToken);
                    
                    if (getAllAvailableRiders.Count > 0)
                    {
                        verify = false;
                        await _notificationHub.NotifyRidersOfIncomingRequest(getAllAvailableRiders);
                        
                    }else if(attempts == 10 && !(getAllAvailableRiders.Count > 0))
                    {
                        await _notificationHub.NotifyUserNoRiderWasFound(_currentUser.UserEmail);
                    }

                }
                catch (Exception ex)
                {
                    await _notificationHub.NotifyUserNoRiderWasFound(_currentUser.UserEmail);
                }

                attempts++;

                if (verify && attempts <= maxAttempts)
                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);


            } while (verify && attempts <= maxAttempts && !cancellationToken.IsCancellationRequested);

            return;

        }

    }
}
