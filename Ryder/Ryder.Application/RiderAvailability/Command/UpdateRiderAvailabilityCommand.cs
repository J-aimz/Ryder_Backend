﻿using AspNetCoreHero.Results;
using MediatR;
using Ryder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ryder.Application.RiderAvailability.Command
{
    public class UpdateRiderAvailabilityCommand : IRequest<IResult<RiderAvailabilityResponse>>
    {
        [JsonIgnore]
        public Guid RiderId { get; set; }
        public RiderAvailabilityStatus AvailabilityStatus { get; set; }
    }
}
