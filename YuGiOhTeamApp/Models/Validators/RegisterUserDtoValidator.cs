using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;

namespace YuGiOhTeamApp.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(AppDbContext dBcontext)
        {
            RuleFor(x => x.Username)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4);
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("Confirm Password and Password do not match.");
            RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    if(dBcontext.Users.Any(u => u.Username == value))
                    {
                        context.AddFailure("Username", "That Username is taken.");
                    }
                });
        }
    }
}
