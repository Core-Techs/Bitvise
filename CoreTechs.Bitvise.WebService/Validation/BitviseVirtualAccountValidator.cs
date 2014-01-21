using CoreTechs.Bitvise.Common;
using FluentValidation;

namespace CoreTechs.Bitvise.WebService.Validation
{
    class BitviseVirtualAccountValidator : AbstractValidator<BitviseVirtualAccount>
    {
        public BitviseVirtualAccountValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Group).NotEmpty();
        }
    }
}