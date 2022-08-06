using System.ComponentModel.DataAnnotations;
using Teams.Domain.Abstractions;

namespace Teams.Domain.Entities;

public class Invite : IEntity
{
    [Key]
    public Guid Guid { get; set; }

    public Guid InviteToGuid { get; set; }
    public Guid ReceiverGuid { get; set; }
}