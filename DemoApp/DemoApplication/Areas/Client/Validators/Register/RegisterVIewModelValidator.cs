using DemoApplication.Areas.Client.ViewModels.Authentication;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using FluentValidation;

namespace DemoApplication.Areas.Client.Validators.Register
{
    public class RegisterVIewModelValidator: AbstractValidator<RegisterViewModel>
    {
        private readonly DataContext _dbContext;
        
        public RegisterVIewModelValidator(DataContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(u => u.Email).Must(IsEmailUnique).WithMessage("Email already taked");

        }


        private bool IsEmailUnique(string email)
        {
            return !_dbContext.Users.Any(u => u.Email == email);
        }
    }
}
