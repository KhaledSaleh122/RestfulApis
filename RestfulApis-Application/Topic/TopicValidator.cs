using FluentValidation;

namespace RestfulApis_Application.TopicSpace
{
    public class TopicValidator : AbstractValidator<TopicDto>
    {
        public TopicValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(x => x.Description)
                .MaximumLength(120);
        }
    }
}
