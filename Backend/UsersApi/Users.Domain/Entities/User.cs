using Users.Domain.Abstractions;

namespace Users.Domain.Entities;

public class User : IEntity
{
    public Guid Guid { get; set; }
    public string Username { get; set; }
}