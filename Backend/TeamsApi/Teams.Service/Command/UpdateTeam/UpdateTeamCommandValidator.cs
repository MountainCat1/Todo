using FluentValidation;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        
        RuleFor(x => x.Dto.Name).NotEmpty();
        RuleFor(x => x.Dto.Name).Length(3, 32);
        
        RuleFor(x => x.Dto.Description).Length(0, 256);
    }
}