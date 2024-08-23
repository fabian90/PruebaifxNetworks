using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.Entities;
namespace ifxNetworks.Api.Validators
{
    public class EntityValidatorUpdate : AbstractValidator<EntityRequestDTOUpdate>
    {
        public EntityValidatorUpdate()
        {
            Include(new EntityNameIsSpecifiedUpdate());
            Include(new EntityIsActiveIsSpecifiedUpdate());
            Include(new EntityIdIsSpecifiedUpdate());
        }
    }

    public class EntityNameIsSpecifiedUpdate : AbstractValidator<EntityRequestDTOUpdate>
    {
        public EntityNameIsSpecifiedUpdate()
        {
            RuleFor(entity => entity.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El nombre de la entidad es obligatorio.")
                .Length(2, 100)
                .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres.");
        }
    }

    public class EntityIsActiveIsSpecifiedUpdate : AbstractValidator<EntityRequestDTOUpdate>
    {
        public EntityIsActiveIsSpecifiedUpdate()
        {
            RuleFor(entity => entity.IsActive)
                .NotNull()
                .WithMessage("El estado de la entidad es obligatorio.");
        }
    }
    public class EntityIdIsSpecifiedUpdate : AbstractValidator<EntityRequestDTOUpdate>
    {
        public EntityIdIsSpecifiedUpdate()
        {
            RuleFor(Entity => Entity.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El ID de la entidad es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El ID de la entidad debe ser un número positivo.");
        }
    }

}
