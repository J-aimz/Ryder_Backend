using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using Ryder.Domain.Context;
using Ryder.Domain.Entities;
using Ryder.Infrastructure.Policy;
using System.Security.Cryptography;

namespace Ryder.Infrastructure.Seed
{
    public static class Seeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            //Get db context
            var dbContext = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<ApplicationContext>();

            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (dbContext.Users.Any())
            {
                return;
            }
            else
            {
                await dbContext.Database.EnsureCreatedAsync();

                var userManager = app.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var roleManager = app.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                //Creating list of roles

                List<string> roles = new() { Policies.Rider, Policies.Customer };

                //Creating roles
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
                }

                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);


                var user = new AppUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Olawale",
                    LastName = "Odeyemi",
                    UserName = "CeoCodes",
                    Email = "cryptmav@gmail.com",
                    PhoneNumber = "01234567890",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    ProfilePictureUrl = "www.avartar.com/publicId",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        City = "Warri",
                        State = "Delta",
                        PostCode = "+234",
                        Longitude = "3",
                        Latitude = "4",
                        Country = "Nigeria",
                        AddressDescription = "magodo lagos"
                    },
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30),
                };

                var user1 = new AppUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "James",
                    LastName = "Elemu",
                    UserName = "Reactgod",
                    Email = "adesojimav@gmail.com",
                    PhoneNumber = "01234567890",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    ProfilePictureUrl = "www.avartar.com/publicId",
                    Address = new Address
                    {
                        City = "warri",
                        State = "Delta",
                        PostCode = "900332",
                        Country = "Nigeria",
                        Longitude = "0",
                        Latitude = "1",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsDeleted = false,
                        AddressDescription = "magodo lagos"
                    },
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30),
                };


                var rider = new Rider
                {
                    ValidIdUrl = "mm.419.com",
                    PassportPhoto = "fdfef.eefe.f",
                    BikeDocument = "sdfgdwerwrewre",
                    AvailabilityStatus = Domain.Enums.RiderAvailabilityStatus.Unavailable,
                    AppUserId = user1.Id,
                };


                await userManager.CreateAsync(user, "P4ssw0rd@123");
                await userManager.AddToRoleAsync(user, roles[1]);
                await userManager.CreateAsync(user1, "@jamesMilna123");
                await userManager.AddToRoleAsync(user1, roles[0]);
                await dbContext.Riders.AddAsync(rider);


                //Saving everything into the database

                await dbContext.SaveChangesAsync();
            }
        }

    }
}
