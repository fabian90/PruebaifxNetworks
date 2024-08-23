using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.Entities;
using System.Text.RegularExpressions;

namespace ifxNetworks.Api.Validators
{
    public class EmployeeValidatorUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeeValidatorUpdate()
        {
            Include(new EmployeeNameIsSpecifiedUpdate());
            Include(new EmployeeLastNameIsSpecifiedUpdate());
            Include(new EmployeeEmailIsSpecifiedUpdate());
            Include(new EmployeePhoneNumberIsSpecifiedUpdate());
            Include(new EmployeePositionIsSpecifiedUpdate());
            Include(new EmployeeEntityIdIsSpecifiedUpdate());
            Include(new EmployeeIdIsSpecifiedUpdate());
        }

        private bool HasValidPassword(string pw)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");

            return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
        }
    }
    public class EmployeeLastNameIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeeLastNameIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El apellido es obligatorio.")
                .Length(2, 50)
                .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres.");
        }
    }

    public class EmployeeEmailIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeeEmailIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress()
                .WithMessage("El formato del correo electrónico es inválido.");
        }
    }

    public class EmployeeNameIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeeNameIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("No ha indicado el nombre.")
                .Length(5, 50)
                .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
        }
    }

    public class EmployeePhoneNumberIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeePhoneNumberIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.Phone)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^\+?\d{10,15}$")
                .When(Employee => !string.IsNullOrEmpty(Employee.Phone))
                .WithMessage("El número de teléfono debe tener entre 10 y 15 dígitos y puede incluir un símbolo '+' al inicio.");
        }
    }

    public class EmployeePositionIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeePositionIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.Position)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("La posición es obligatoria.")
                .Length(2, 100)
                .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres.");
        }
    }

    public class EmployeeEntityIdIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeeEntityIdIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.EntityId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El ID de la entidad es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El ID de la entidad debe ser un número positivo.");
        }
    }

    public class EmployeeIdIsSpecifiedUpdate : AbstractValidator<EmployeeRequestDTOUpdate>
    {
        public EmployeeIdIsSpecifiedUpdate()
        {
            RuleFor(Employee => Employee.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El ID de la employee es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El ID de la employee debe ser un número positivo.");
        }
    }

}