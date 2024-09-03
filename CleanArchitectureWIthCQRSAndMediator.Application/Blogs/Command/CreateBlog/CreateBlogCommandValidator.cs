using FluentValidation;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Name required")
                .MinimumLength(3).WithMessage("Name Length Should be >= 3");

            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description is Required");
        }
    }
}
