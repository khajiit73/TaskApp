using FluentValidation;
using TaskApp.Services.Dtos;

namespace TaskApp.Api.Validators
{
    public class CreateBoardDtoValidator : AbstractValidator<CreateBoardDto>
    {
        public CreateBoardDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(30);
        }
    }
}
