using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;

namespace YuGiOhTeamApp.Models.Validators
{
    public class CreateDecklistValidator : AbstractValidator<CreateDecklistDto>
    {
        public CreateDecklistValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    if (dbContext.Decklists.Any(d => d.Name == value))
                    {
                        context.AddFailure("Name", "That name is taken.");
                    }
                });
        }
    }
}
