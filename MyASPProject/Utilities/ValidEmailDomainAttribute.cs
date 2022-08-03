using System.ComponentModel.DataAnnotations;

namespace MyASPProject.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowDomain)
        {
            this.allowedDomain = allowDomain;
        }

        // Override
        public override bool IsValid(object? value)
        {
            var email = value.ToString().Split("@");
            return email[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
