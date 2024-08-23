using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.Entities;
using System.Text.RegularExpressions;

namespace ifxNetworks.Api.Validators
{
    public class EmployeeValidatorCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeeValidatorCreate()
        {
            Include(new EmployeeNameIsSpecifiedCreate());
            Include(new EmployeeLastNameIsSpecifiedCreate());
            Include(new EmployeeEmailIsSpecifiedCreate());
            Include(new EmployeePhoneNumberIsSpecifiedCreate());
            Include(new EmployeePositionIsSpecifiedCreate());
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
    public class EmployeeLastNameIsSpecifiedCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeeLastNameIsSpecifiedCreate()
        {
            RuleFor(Employee => Employee.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El apellido es obligatorio.")
                .Length(2, 50)
                .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres.");
        }
    }

    public class EmployeeEmailIsSpecifiedCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeeEmailIsSpecifiedCreate()
        {
            RuleFor(Employee => Employee.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress()
                .WithMessage("El formato del correo electrónico es inválido.");
        }
    }

    public class EmployeeNameIsSpecifiedCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeeNameIsSpecifiedCreate()
        {
            RuleFor(Employee => Employee.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("No ha indicado el nombre.")
                .Length(5, 50)
                .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
        }
    }

    public class EmployeePhoneNumberIsSpecifiedCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeePhoneNumberIsSpecifiedCreate()
        {
            RuleFor(Employee => Employee.Phone)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^\+?\d{10,15}$")
                .When(Employee => !string.IsNullOrEmpty(Employee.Phone))
                .WithMessage("El número de teléfono debe tener entre 10 y 15 dígitos y puede incluir un símbolo '+' al inicio.");
        }
    }

    public class EmployeePositionIsSpecifiedCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeePositionIsSpecifiedCreate()
        {
            RuleFor(Employee => Employee.Position)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("La posición es obligatoria.")
                .Length(2, 100)
                .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres.");
        }
    }

    public class EmployeeEntityIdIsSpecifiedCreate : AbstractValidator<EmployeeRequestDTOCreate>
    {
        public EmployeeEntityIdIsSpecifiedCreate()
        {
            RuleFor(Employee => Employee.EntityId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El ID de la entidad es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El ID de la entidad debe ser un número positivo.");
        }
    }
}
