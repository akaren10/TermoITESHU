using COMMON.Entidades;
using FluentValidation;

namespace COMMON.Validadores
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : Base
    {

        public BaseValidator()
        {
            RuleFor(b => b.Id).NotEmpty();
            RuleFor(b => b.FechaHora).NotEmpty();

        }
    }
}
