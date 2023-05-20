using COMMON.Entidades;
using FluentValidation;

namespace COMMON.Validadores
{
    public class DispositivoValidator : BaseValidator<Dispositivo>
    {

        public DispositivoValidator()
        {
            RuleFor(d => d.FechaColocacion).NotEmpty();
            RuleFor(d => d.Nombre).NotEmpty();
            RuleFor(d => d.NommbreRele1).NotEmpty();
            RuleFor(d => d.NommbreRele2).NotEmpty();
            RuleFor(d => d.NommbreRele3).NotEmpty();
            RuleFor(d => d.NommbreRele4).NotEmpty();
            RuleFor(d => d.Ubicacion).NotEmpty();

        }
    }
}

