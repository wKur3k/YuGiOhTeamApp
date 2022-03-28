using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Models.Validators
{
    public class CreateTeamValidator : AbstractValidator<CreateTeamDto>
    {
        public CreateTeamValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
