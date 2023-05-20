using COMMON.Entidades;
using FluentValidation;

namespace COMMON.Validadores
{
    public class LecturaValidator : BaseValidator<Lectura>

    {
        public LecturaValidator()
        {
            RuleFor(l => l.IdDispositivo).NotEmpty();
            RuleFor(l => l.Temperatura).NotEmpty();
            RuleFor(l => l.Humedad).NotEmpty();
            RuleFor(l => l.Luminosidad).NotEmpty();

        }
    }
}
