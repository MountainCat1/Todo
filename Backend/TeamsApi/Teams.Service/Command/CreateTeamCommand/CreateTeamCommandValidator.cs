using FluentValidation;

namespace Teams.Service.Command.CreateTeamCommand;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        
        RuleFor(x => x.Dto.Name).NotEmpty();
        RuleFor(x => x.Dto.Name).Length(3, 32);
        
        RuleFor(x => x.Dto.Description).Length(0, 256);
    }
}