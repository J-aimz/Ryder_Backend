﻿using AspNetCoreHero.Results;
using FluentValidation;
using MediatR;

namespace Ryder.Application.Payment.Command
{
    public class PaymentCommand : IRequest<IResult<PaymentResponse>>
    {
        public int AmountInKobo { get; set; }
        public string Email { get; set; }
        public string CallbackUrl { get; set; }
        public string Currency { get; set; } = "NGN";
        public Guid OrderId { get; set; }
    }

    public class PaymentCommandValidator : AbstractValidator<PaymentCommand>
    {
        public PaymentCommandValidator()
        {
            RuleFor(r => r.AmountInKobo)
                .GreaterThan(0)
                .WithMessage("AmountInKobo must be greater than 0");

            RuleFor(r => r.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(r => r.CallbackUrl)
                .NotNull()
                .NotEmpty()
                .Must(BeAValidUri)
                .WithMessage("Invalid URL format");

            RuleFor(r => r.OrderId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Order ID not found");
        }

        private bool BeAValidUri(string callbackUrl)
        {
            return Uri.TryCreate(callbackUrl, UriKind.Absolute, out _);
        }
    }
}
