using Orders.Models.Requests;
using FluentValidation;
using System.Linq;

namespace Orders.Validators
{
  public class ScheduleRequestValidator : AbstractValidator<ScheduleRequest>
  {
    public ScheduleRequestValidator()
    {
      RuleFor(x => x.Orders)
        .NotNull();

      RuleForEach(x => x.Orders)
        .ChildRules(orders =>
        {
          orders.RuleFor(x => x.CustomerName)
          .NotNull()
          .NotEmpty()
          .Must(x => NotBeWhiteSpace(x))
            .WithMessage(x => $"The parameter '{nameof(x.CustomerName)}' must not be whitespace");
        });
    }

    private bool NotBeWhiteSpace(string str)
    {
      return null != str && !str.All(char.IsWhiteSpace);
    }
  }
}