using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
namespace ifxNetworks.Api.Validators
{
    public class EntityValidatorCreate : AbstractValidator<EntityRequestDTOCreate>
    {
        public EntityValidatorCreate()
        {
            Include(new EntityNameIsSpecifiedCreate());
            Include(new EntityIsActiveIsSpecifiedCreate());
        }
    }

    public class EntityNameIsSpecifiedCreate : AbstractValidator<EntityRequestDTOCreate>
    {
        public EntityNameIsSpecifiedCreate()
        {
            RuleFor(entity => entity.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El nombre de la entidad es obligatorio.")
                .Length(2, 100)
                .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres.");
        }
    }

    public class EntityIsActiveIsSpecifiedCreate : AbstractValidator<EntityRequestDTOCreate>
    {
        public EntityIsActiveIsSpecifiedCreate()
        {
            RuleFor(entity => entity.IsActive)
                .NotNull()
                .WithMessage("El estado de la entidad es obligatorio.");
        }
    }
}
