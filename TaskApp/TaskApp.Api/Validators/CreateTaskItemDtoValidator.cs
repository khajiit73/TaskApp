using FluentValidation;
using TaskApp.Services.Dtos;

namespace TaskApp.Api.Validators
{
    public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
    {
        public CreateTaskItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(30);

            RuleFor(x => x.Description)
                .MaximumLength(100);
        }
    }
}
