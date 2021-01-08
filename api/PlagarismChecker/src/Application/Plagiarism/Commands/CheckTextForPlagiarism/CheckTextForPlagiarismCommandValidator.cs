using FluentValidation;

namespace PlagarismChecker.Application.Plagiarism.Commands.CheckTextForPlagiarism
{
    public class CheckTextForPlagiarismCommandValidator : AbstractValidator<CheckTextForPlagiarismCommand>
    {
        public CheckTextForPlagiarismCommandValidator()
        {
            RuleFor(x => x.TextToCheck)
                .NotEmpty().WithMessage("TextToSearch must not be empty!");
        }
    }
}
