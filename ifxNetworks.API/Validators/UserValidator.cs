using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using System.Text.RegularExpressions;

namespace ifxNetworks.Api.Validators
{
    public class UserValidator : AbstractValidator<UserRequestDTO>
    {
        public UserValidator()
        {
            Include(new UserNameIsSpecified());
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

    public class UserNameIsSpecified : AbstractValidator<UserRequestDTO>
    {
        public UserNameIsSpecified()
        {
            RuleFor(user => user.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("No ha indicado el nombre.")
                .Length(5, 50)
                .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
        }
    }

   
}