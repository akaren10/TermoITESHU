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
            RuleFor(d => d.NombreRele1).NotEmpty();
            RuleFor(d => d.NombreRele2).NotEmpty();
            RuleFor(d => d.NombreRele3).NotEmpty();
            RuleFor(d => d.NombreRele4).NotEmpty();
            RuleFor(d => d.Ubicacion).NotEmpty();

        }
    }
}

