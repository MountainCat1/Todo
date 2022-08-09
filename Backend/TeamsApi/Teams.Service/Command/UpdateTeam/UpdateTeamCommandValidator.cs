using FluentValidation;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(x => x.UpdateDto).NotNull();
        
        RuleFor(x => x.UpdateDto.Name).NotEmpty();
        RuleFor(x => x.UpdateDto.Name).Length(3, 32);
        
        RuleFor(x => x.UpdateDto.Description).Length(0, 256);
    }
}