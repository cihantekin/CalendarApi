using Calendar.Business.Dto;
using FluentValidation;

namespace Calendar.Business.Validators
{
    public class EventValidator : AbstractValidator<EventDto>
    {
        public EventValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Organizer).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
        }
    }
}
